using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class InventoryItem
{
    public ItemSO itemSO;
    public int cnt;

    public InventoryItem(ItemSO itemSO, int value = 1) {
        this.itemSO = itemSO;
        cnt = value;
    }

    public void AddStack(int value = 1) => cnt += value;
    public void RemoveStack(int value = 1) => cnt -= value;
}
