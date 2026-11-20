using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapShiftWindow : EditorWindow
{
    /*
     * An in-engine editor window for shifting entire columns or rows of tilemaps and
     * GameObjects when designing levels. This file must live in /Assets/Editor/
     *
     * Open it from Tools > Tilemap > Shift Window.
     * Select every layer (e.g. foreground, background) of one map in the Hierarchy, [ctrl+click] to select multiple.
     * [ctrl+click] any NPCs, doors, signs, warps, etc that need to be moved.
     * Set the axis, start line, and shift amount, then click "Shift". Use [ctrl+Z] to undo the shift.
     */

    enum Axis { Columns, Rows }

    [SerializeField] Axis axis = Axis.Columns;
    [SerializeField] int start = 0;
    [SerializeField] int amount = -1;
    [SerializeField] bool moveObjects = true;

    [MenuItem("Tools/Tilemap/Shift Window")]
    static void Open() => GetWindow<TilemapShiftWindow>("Tilemap Shift");

    void OnSelectionChange() => Repaint();

    void OnGUI()
    {
        EditorGUILayout.Space();
        axis = (Axis)EditorGUILayout.EnumPopup("Shift", axis);
        start = EditorGUILayout.IntField(axis == Axis.Columns ? "Start column (x)" : "Start row (y)", start);
        amount = EditorGUILayout.IntField("Amount (cells)", amount);
        moveObjects = EditorGUILayout.Toggle("Move selected GameObjects", moveObjects);

        EditorGUILayout.HelpBox(
            "Everything at or past the start line moves by the amount. " +
            (axis == Axis.Columns ? "Positive = right, negative = left. " : "Positive = up, negative = down. ") +
            "To close a gap, set the start to the first cell AFTER the gap and the amount to minus the gap size. " +
            "Cells the block moves into are erased.",
            MessageType.Info);

        var tilemaps = GetSelectedTilemaps();
        var objects = GetSelectedObjects();
        EditorGUILayout.LabelField("Selected tilemaps", tilemaps.Count.ToString());
        EditorGUILayout.LabelField("Selected GameObjects", objects.Count.ToString());

        EditorGUILayout.Space();

        using (new EditorGUI.DisabledScope(objects.Count == 0))
        {
            if (GUILayout.Button("Set start from selected object's cell"))
            {
                Grid grid = tilemaps.Count > 0 ? tilemaps[0].layoutGrid : FindFirstObjectByType<Grid>();
                if (grid != null)
                {
                    Vector3Int cell = grid.WorldToCell(objects[0].position);
                    start = axis == Axis.Columns ? cell.x : cell.y;
                }
            }
        }

        using (new EditorGUI.DisabledScope(tilemaps.Count == 0 || amount == 0))
        {
            if (GUILayout.Button("Shift", GUILayout.Height(30)))
                Shift(tilemaps, objects);
        }

        if (tilemaps.Count == 0)
            EditorGUILayout.HelpBox("Select at least one Tilemap in the Hierarchy.", MessageType.Warning);
    }

    static List<Tilemap> GetSelectedTilemaps()
    {
        var list = new List<Tilemap>();
        foreach (var go in Selection.gameObjects)
        {
            var map = go.GetComponent<Tilemap>();
            if (map != null) list.Add(map);
        }
        return list;
    }

    // Top-level selected transforms that are not tilemaps or grids.
    static List<Transform> GetSelectedObjects()
    {
        var list = new List<Transform>();
        foreach (var t in Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.Editable))
        {
            if (t.GetComponent<Tilemap>() != null || t.GetComponent<Grid>() != null) continue;
            list.Add(t);
        }
        return list;
    }

    void Shift(List<Tilemap> tilemaps, List<Transform> objects)
    {
        string label = axis == Axis.Columns ? "Shift columns" : "Shift rows";
        Undo.SetCurrentGroupName(label);
        int undoGroup = Undo.GetCurrentGroup();

        // Create an offset vec3 along the selected axis.
        Vector3Int offset = axis == Axis.Columns
            ? new Vector3Int(amount, 0, 0)
            : new Vector3Int(0, amount, 0);

        int tilemapCount = 0;
        foreach (var map in tilemaps)
        {
            if (ShiftTilemap(map, offset, label)) tilemapCount++;
        }

        int objectCount = 0;
        if (moveObjects && objects.Count > 0)
        {
            Grid grid = tilemaps[0].layoutGrid;
            if (grid == null)
            {
                Debug.LogWarning("Tilemap Shift: no Grid found, so GameObjects were not moved.");
            }
            else
            {
                foreach (var t in objects)
                {
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

        // Use [ctrl+z] to undo the operation.
        Undo.CollapseUndoOperations(undoGroup);
        Debug.Log($"{label}: moved {tilemapCount} tilemap(s) and {objectCount} object(s).");
    }

    bool ShiftTilemap(Tilemap map, Vector3Int offset, string label)
    {
        map.CompressBounds();
        BoundsInt b = map.cellBounds;

        int size = axis == Axis.Columns ? b.xMax - start : b.yMax - start;
        if (size <= 0) return false;   // nothing at or past the start line

        BoundsInt source = axis == Axis.Columns
            ? new BoundsInt(start, b.y, b.z, size, b.size.y, b.size.z)
            : new BoundsInt(b.x, start, b.z, b.size.x, size, b.size.z);

        BoundsInt target = source;
        target.position += offset;

        // Read tile data first (tile, tint, and rotation/flip).
        var moved = new List<TileChangeData>();
        foreach (var pos in source.allPositionsWithin)
        {
            TileBase tile = map.GetTile(pos);
            if (tile == null) continue;
            moved.Add(new TileChangeData(pos + offset, tile, map.GetColor(pos), map.GetTransformMatrix(pos)));
        }

        Undo.RegisterCompleteObjectUndo(map, label);

        // Clear out both the old tiles and the tiles being replaced.
        ClearBlock(map, source);
        ClearBlock(map, target);
        map.SetTiles(moved.ToArray(), true);

        EditorUtility.SetDirty(map);
        EditorSceneManager.MarkSceneDirty(map.gameObject.scene);
        return true;
    }

    static void ClearBlock(Tilemap map, BoundsInt area)
    {
        map.SetTilesBlock(area, new TileBase[area.size.x * area.size.y * area.size.z]);
    }
}