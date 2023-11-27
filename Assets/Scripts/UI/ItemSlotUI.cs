using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;
    private Button button;
    private ItemSO item;

    private Inventory inventory => Inventory.instance;

    private void Awake() {
        button = GetComponent<Button>();
    }

    public void UpdateItemSlot(ItemSO item, int value = 1) {
        this.item = item;
        if (item == null || value == 0) {
            itemImage.sprite = inventory.defaultSprite;
            itemText.text = "";
        } else {
            itemImage.sprite = item.icon;
            itemText.text = value.ToString();
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (item != null) {
            inventory.description.gameObject.SetActive(true);
            inventory.description.position = transform.position;
            inventory.description.GetComponentInChildren<TextMeshProUGUI>().text = item.description;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        inventory.description.gameObject.SetActive(false);
        inventory.description.GetComponentInChildren<TextMeshProUGUI>().text = "";
    }
}
