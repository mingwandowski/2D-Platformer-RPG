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
    [SerializeField] public Sprite defaultSprite;
    public ItemSlotUI[] itemSlots;
    public Transform description;

    public List<ItemSO> itemListTest;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    private void Start() {
        itemSlots = itemSlotParent.GetComponentsInChildren<ItemSlotUI>();
        description = itemSlotParent.parent.parent.Find("Description");

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
