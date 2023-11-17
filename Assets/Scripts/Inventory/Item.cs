using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField] private ItemSO itemSO;

    private void OnValidate() {
        GetComponent<SpriteRenderer>().sprite = itemSO.icon;
        gameObject.name = "Item - " + itemSO.name;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Inventory.instance.AddItem(itemSO);
            Destroy(gameObject, .1f);
            // itemData.isCollected = true;
        }
    }
}
