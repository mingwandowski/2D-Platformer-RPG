using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;
    private ItemSO item;

    public void UpdateItemSlot(ItemSO item, int value) {
        if (item != null) {
            itemImage.color = Color.white;
            itemImage.sprite = item.icon;
            itemText.text = value == 0 ? "" : value.ToString();
        }
    }
}
