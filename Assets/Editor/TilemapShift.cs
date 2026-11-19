using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilemapShiftTool
{
    /*
     * Unity editor script to quickly shift entire columns or rows of the map (and the GameObjects on them) when
     * designing and editing levels. Make sure this file stays in /Assets/Editor.
     *
     * Change the constants below to the desired values. Cell coordinates are shown in the Tile Palette and Scene view when you hover a tile.
     * In the Hierarchy, select all the tilemap layers (e.g. foreground, background) for your map simultaneously [ctrl+click].
     * To move GameObjects too, ctrl+click each NPC, door, sign, warp, etc. as well.
     * Run Tools → Tilemap → Shift Columns or Shift Rows. Because each selected layer gets the same shift, they stay aligned.
     * Check the result, and undo with [ctrl+Z] if needed.
     */

    // Shift columns along x-axis.
    const int StartX = 20;           // first column to move (use cell coords)
    const int ShiftColumnsBy = -5;   // positive moves right, negative moves left

    // Shift rows along y-axis.
    const int StartY = 10;           // first row to move (use cell coords)
    const int ShiftRowsBy = 0;       // positive moves up, negative moves down

    // If true, selected non-tilemap objects at or past the start line also move.
    const bool MoveGameObjects = true;

    enum Axis { Columns, Rows }

    [MenuItem("Tools/Tilemap/Shift Columns")]
    static void ShiftColumns() => Shift(Axis.Columns, StartX, ShiftColumnsBy);

    [MenuItem("Tools/Tilemap/Shift Rows")]
    static void ShiftRows() => Shift(Axis.Rows, StartY, ShiftRowsBy);

    static void Shift(Axis axis, int start, int amount)
    {
        string label = axis == Axis.Columns ? "Shift columns" : "Shift rows";
        Undo.SetCurrentGroupName(label);
        int undoGroup = Undo.GetCurrentGroup();

        Vector3Int offset = axis == Axis.Columns
            ? new Vector3Int(amount, 0, 0)
            : new Vector3Int(0, amount, 0);

        Grid grid = null;
        int tilemapCount = 0;
        int objectCount = 0;

        // Shift the tiles on every selected Tilemap.
        foreach (var go in Selection.gameObjects)
        {
            var map = go.GetComponent<Tilemap>();
            if (map == null) continue;

            if (grid == null) grid = map.layoutGrid;

            map.CompressBounds();
            BoundsInt b = map.cellBounds;

            int size = axis == Axis.Columns ? b.xMax - start : b.yMax - start;
            if (size <= 0) continue;   // nothing at or past the start line

            BoundsInt source = axis == Axis.Columns
                ? new BoundsInt(start, b.y, b.z, size, b.size.y, b.size.z)
                : new BoundsInt(b.x, start, b.z, b.size.x, size, b.size.z);

            BoundsInt target = source;
            target.position += offset;

            Undo.RegisterCompleteObjectUndo(map, label);

            TileBase[] tiles = map.GetTilesBlock(source);
            map.SetTilesBlock(source, new TileBase[tiles.Length]);  // clear old spot
            map.SetTilesBlock(target, tiles);                       // write new spot

            EditorUtility.SetDirty(map);
            EditorSceneManager.MarkSceneDirty(go.scene);
            tilemapCount++;
        }

        // Move the other selected GameObjects by the same number of cells.
        if (MoveGameObjects)
        {
            if (grid == null) grid = Object.FindFirstObjectByType<Grid>();

            if (grid == null)
            {
                Debug.LogWarning("TilemapShiftTool: no Grid found, so GameObjects were not moved.");
            }
            else
            {
                // TopLevel: if a parent and its child are both selected, only the parent is used
                foreach (var t in Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.Editable))
                {
                    if (t.GetComponent<Tilemap>() != null || t.GetComponent<Grid>() != null) continue;

                    Vector3Int cell = grid.WorldToCell(t.position);
                    int coord = axis == Axis.Columns ? cell.x : cell.y;
                    if (coord < start) continue;

                    // world-space distance of the shift, accounting for the Grid's cell size and scale
                    Vector3 delta = grid.CellToWorld(cell + offset) - grid.CellToWorld(cell);

                    Undo.RecordObject(t, label);
                    t.position += delta;

                    EditorUtility.SetDirty(t);
                    EditorSceneManager.MarkSceneDirty(t.gameObject.scene);
                    objectCount++;
                }
            }
        }

        // One [ctrl+Z] input undoes the entire operation.
        Undo.CollapseUndoOperations(undoGroup);
        Debug.Log($"{label}: moved {tilemapCount} tilemap(s) and {objectCount} object(s).");
    }
}