using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;
    private ItemSO item;

    public void UpdateItemSlot(ItemSO item, int value = 1) {
        this.item = item;
        if (item == null || value == 0) {
            itemImage.color = Color.clear;
            itemText.text = "";
        } else {
            itemImage.color = Color.white;
            itemImage.sprite = item.icon;
            itemText.text = value.ToString();
        }
    }
}
