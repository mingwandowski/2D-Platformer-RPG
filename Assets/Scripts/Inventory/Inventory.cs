using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public Dictionary<ItemSO, InventoryItem> inventory = new();
    [SerializeField] private Transform itemSlotParent;
    [SerializeField] private GameObject itemSlotPrefab;
    private int inventorySize = 2;
    public ItemSlotUI[] itemSlots;

    public List<ItemSO> itemListTest;

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

        // For Test
        itemListTest?.ForEach(item => AddItem(item));
    }

    public void UpdateSlotUI() {
        List<ItemSO> itemList = inventory.Keys.ToList();
        for (int i = 0; i < itemSlots.Length; i++) {
            if (i < itemList.Count) {
                ItemSO item = itemList[i];
                itemSlots[i].UpdateItemSlot(item, inventory[item].cnt);
            } else {
                itemSlots[i].UpdateItemSlot(null);
            }
        }
    }

    public void AddItem(ItemSO item, int value = 1) {
        if (inventory.ContainsKey(item)) {
            inventory[item].AddStack(value);
        } else {
            inventory.Add(item, new InventoryItem(item, value));
        }
        UpdateSlotUI();
    }

    public void RemoveItem(ItemSO item, int value = 1) {
        if (inventory.ContainsKey(item)) {
            if (inventory[item].cnt > value) {
                inventory[item].RemoveStack(value);
            } else if (inventory[item].cnt == value) {
                inventory.Remove(item);
            } else {
                Debug.Log("not enough");
            }
        }
        UpdateSlotUI();
    }
}
