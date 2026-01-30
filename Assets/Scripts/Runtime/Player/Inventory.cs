using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private readonly Dictionary<ItemSO, int> m_PickedUpItems = new();
    private void OnEnable() {
        Item.OnPickedUpItem += AddPickedUpItemToInventory;
    }

    private void OnDisable() {
        Item.OnPickedUpItem -= AddPickedUpItemToInventory;
    }

    private void AddPickedUpItemToInventory(ItemSO pickedUpItem) {
        m_PickedUpItems[pickedUpItem] = m_PickedUpItems.GetValueOrDefault(pickedUpItem) + 1;
    }

    public bool UseItem(ItemSO itemToUse) {
        if(m_PickedUpItems.ContainsKey(itemToUse) && m_PickedUpItems[itemToUse] > 0) {
            m_PickedUpItems[itemToUse] -= 1;
            return true;
        }
        return false;
    }
}
