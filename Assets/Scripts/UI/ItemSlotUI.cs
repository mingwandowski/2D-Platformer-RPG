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
            itemImage.sprite = Inventory.instance.defaultSprite;
            itemText.text = "";
        } else {
            itemImage.sprite = item.icon;
            itemText.text = value.ToString();
        }
    }
}
