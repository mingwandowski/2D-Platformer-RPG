using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public Dictionary<ItemSO, int> inventory = new();
    [SerializeField] private Transform itemSlotParent;
    [SerializeField] private GameObject itemSlotPrefab;
    private int inventorySize = 12;
    private ItemSlotUI[] itemSlots;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    private void Start() {
        for (int i = 0; i < inventorySize; i++) {
            Instantiate(itemSlotPrefab, itemSlotParent);
        }
        itemSlots = itemSlotParent.GetComponentsInChildren<ItemSlotUI>();
    }

    public void UpdateSlotUI() {
        List<ItemSO> itemList = inventory.Keys.ToList();
        for (int i = 0; i < itemSlots.Length; i++) {
            if (i < itemList.Count) {
                itemSlots[i].UpdateItemSlot(itemList[i], inventory[itemList[i]]);
            } else {
                itemSlots[i].UpdateItemSlot(null);
            }
        }
    }

    public void AddItem(ItemSO item) {
        if (inventory.ContainsKey(item)) {
            inventory[item]++;
        } else {
            inventory.Add(item, 1);
        }
        UpdateSlotUI();
    }

    public void RemoveItem(ItemSO item) {
        if (inventory.ContainsKey(item)) {
            inventory[item]--;
            if (inventory[item] <= 0) {
                inventory.Remove(item);
            }
        }
        UpdateSlotUI();
    }
}
