using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    public GameObject infoWindow;
    private ItemSlot curSlot;
    public GameObject equipMark;

    public int index;
    public bool equipped;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        equipMark.SetActive(equipped);
    }

    public void Set(ItemSlot slot)
    {
        curSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.itemIcon;
        icon.SetNativeSize();
        quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;

        if (equipMark != null)
        {
            equipMark.SetActive(equipped);
        }
        button.onClick.AddListener(OnButtonClick);
    }

    public void Clear()
    {
        curSlot = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    public void OnButtonClick()
    {
        Inventory.Instance.SelectItem(index);
        infoWindow.SetActive(true);
    }

    public void Equip(bool flag)
    {
        equipped = flag;

        EquipMark();
    }

    public void EquipMark()
    {
        if (equipMark != null)
        {
            equipMark.SetActive(equipped);
        }
    }
}
