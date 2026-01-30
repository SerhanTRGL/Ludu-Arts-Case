using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private readonly Dictionary<ItemSO, int> m_Items = new();
    
    public event Action OnInventoryUpdated;
    public Dictionary<ItemSO, int> Items => m_Items;

    public int UniqueItemCount => m_Items.Count;
    private void OnEnable() {
        Item.OnPickedUpItem += AddPickedUpItemToInventory;
    }

    private void OnDisable() {
        Item.OnPickedUpItem -= AddPickedUpItemToInventory;
    }

    private void AddPickedUpItemToInventory(ItemSO pickedUpItem) {
        m_Items[pickedUpItem] = m_Items.GetValueOrDefault(pickedUpItem) + 1;

        OnInventoryUpdated?.Invoke();
    }

    public bool UseItem(ItemSO itemToUse) {
        if(m_Items.ContainsKey(itemToUse) && m_Items[itemToUse] > 0) {
            m_Items[itemToUse] -= 1;

            OnInventoryUpdated?.Invoke();

            return true;
        }
        return false;
    }

}
