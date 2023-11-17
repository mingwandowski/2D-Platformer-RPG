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
    private ItemSlotUI[] itemslots;

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
        itemslots = itemSlotParent.GetComponentsInChildren<ItemSlotUI>();
    }

    public void UpdateSlotUI() {
        int index = 0;
        foreach (var kvp in inventory) {
            ItemSO item = kvp.Key;
            int quantity = kvp.Value;

            if (index < itemslots.Length) {
                itemslots[index].UpdateItemSlot(item, quantity);
                index++;
            } else {
                break; // Break loop if there are no more item slots to update
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
