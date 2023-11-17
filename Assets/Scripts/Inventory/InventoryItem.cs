using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public ItemSO itemSO;
    public int stackSize;

    public InventoryItem(ItemSO itemSO) {
        this.itemSO = itemSO;
        stackSize = 1;
    }

    public void AddStack() => stackSize++;
    public void RemoveStack() => stackSize--;
}
