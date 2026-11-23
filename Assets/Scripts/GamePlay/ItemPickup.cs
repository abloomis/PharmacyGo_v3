using UnityEngine;
using System.Collections;
using System;

public class ItemPickup : MonoBehaviour, Interactable
{

    // Allows player to pick up items they find and gets recorded as collected

    [SerializeField] private string itemName;
    [SerializeField] private GameObject InteractPrompt;
    [SerializeField, TextArea] private string pickupDialogue;

    void Start()
    {
        if (ProgressionState.Instance.HasItem(itemName))
        {
            // If the scene is reloaded and the player has already retrieved the item
            gameObject.SetActive(false);
        }
    }

    private IEnumerator PickupSequence()
    {
        Debug.Log("Added item to inventory: " + itemName);
        
        ProgressionState.Instance.CollectItem(itemName);

        if (!string.IsNullOrWhiteSpace(pickupDialogue))
        {
            yield return StartCoroutine(DialogManager.Instance.ShowDialogText(pickupDialogue));
        }

        gameObject.SetActive(false);
    }
    public void Interact(Transform initiator)
    {

        StartCoroutine(PickupSequence());
        
    }


    public void ShowPrompt()
    {
        InteractPrompt.SetActive(true);
    }

    public void HidePrompt()
    {
        InteractPrompt.SetActive(false);
    }
}
