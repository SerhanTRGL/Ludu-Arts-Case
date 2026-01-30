using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    public Inventory PlayerInventory;
    public Transform ContentTransform;
    public GameObject InventoryEntryPrefab;

    private void OnEnable() {
        if(PlayerInventory != null) {
            PlayerInventory.OnInventoryUpdated += UpdateInventoryEntries;
        }
    }
    private void OnDisable() {
        if(PlayerInventory != null) {
            PlayerInventory.OnInventoryUpdated -= UpdateInventoryEntries;
        }
    }

    private void UpdateInventoryEntries() {
        if(PlayerInventory.UniqueItemCount > ContentTransform.childCount) {
            for(int i = 0; i < PlayerInventory.UniqueItemCount - ContentTransform.childCount; i++) {
                Instantiate(InventoryEntryPrefab, ContentTransform);
            }
        }

        int index = 0;
        foreach(var kvp in PlayerInventory.Items) {
            var item = kvp.Key;
            var itemCount = kvp.Value;

            var entryObject = ContentTransform.GetChild(index);
            entryObject.gameObject.SetActive(true);

            entryObject.GetComponent<TextMeshProUGUI>().text = $"{item.ItemDisplayName} x{itemCount}";
            index++;
        }

        for(int i = index; index < ContentTransform.childCount; i++) {
            ContentTransform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
