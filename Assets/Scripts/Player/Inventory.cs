using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot
{
    public ItemData item;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    private static Inventory instance = null;
    public static Inventory Instance
    {
        get
        {
            return instance == null ? null : instance;
        }
    }

    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

    [Header("Selected Item")]
    public GameObject selectedWindow;
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public Image selectedItemIcon;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDesc;
    public Image selectedItemStatIcon;
    public TextMeshProUGUI selectedItemStat;
    public TextMeshProUGUI selectedItemStatValue;
    public TextMeshProUGUI selectedItemUse;

    public Sprite[] itemStatIcons;

    public TextMeshProUGUI logText;

    private int[] equipIndex;

    // Debugging
    public ItemData[] examples;
    public ItemData[] ex2;

    void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        slots = new ItemSlot[uiSlots.Length];
        equipIndex = new int[5];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }

        ClearSelectedItemWindow();

        // Debugging
        for (int i = 0; i < examples.Length; i++)
        {
            if (i == 0) AddItem(examples[i]);
            AddItem(examples[i]);
        }
    }

    public void AddItem(ItemData item)
    {
        var newData = Instantiate(item) as ItemData;

        if (newData.canStack)
        {
            ItemSlot slotToStackTo = GetItemStack(newData);

            if (slotToStackTo != null)
            {
                if (slotToStackTo.item.maxStackAmount < slotToStackTo.quantity)
                {
                    slotToStackTo.quantity++;
                    UpdateUI();
                }
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = newData;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }

        Destroy(newData);

        StartCoroutine(UpdateLog("인벤토리가 가득찼습니다."));
    }

    IEnumerator UpdateLog(string log)
    {
        logText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        logText.gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
                uiSlots[i].Set(slots[i]);
            else
                uiSlots[i].Clear();
        }
    }

    ItemSlot GetItemStack(ItemData item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
                return slots[i];
        }

        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }

        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;

        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemIcon.sprite = selectedItem.item.itemIcon;
        selectedItemIcon.SetNativeSize();
        selectedItemName.text = selectedItem.item.itemName;
        selectedItemDesc.text = selectedItem.item.itemDesc;

        if (selectedItem.item.itemType == ItemData.ItemType.Weapon)
        {
            selectedItemStatIcon.sprite = itemStatIcons[0];
            selectedItemStat.text = "공격력";
            selectedItemStatValue.text = selectedItem.item.baseAttack.ToString();
        }
        else if (selectedItem.item.itemType == ItemData.ItemType.Armor)
        {
            if (selectedItem.item.baseDefense != 0)
            {
                selectedItemStatIcon.sprite = itemStatIcons[1];
                selectedItemStat.text = "방어력";
                selectedItemStatValue.text = selectedItem.item.baseDefense.ToString();
            }
            else if (selectedItem.item.baseHealth != 0)
            {
                selectedItemStatIcon.sprite = itemStatIcons[2];
                selectedItemStat.text = "체력";
                selectedItemStatValue.text = selectedItem.item.baseHealth.ToString();
            }
        }
        else if (selectedItem.item.itemType == ItemData.ItemType.Accessory)
        {
            selectedItemStatIcon.sprite = itemStatIcons[3];
            selectedItemStat.text = "방어력";
            selectedItemStatValue.text = selectedItem.item.baseDefense.ToString();
        }
        else if (selectedItem.item.itemType == ItemData.ItemType.Consume)
        {
            selectedItemStatIcon.sprite = itemStatIcons[4];
            selectedItemStat.text = "체력 회복";
            selectedItemStatValue.text = "+" + selectedItem.item.baseHealth.ToString();
        }

        if (selectedItem.item.itemType == ItemData.ItemType.Weapon || selectedItem.item.itemType == ItemData.ItemType.Armor || selectedItem.item.itemType == ItemData.ItemType.Accessory)
        { 
            if (!uiSlots[index].equipped) selectedItemUse.text = "장착하시겠습니까?";
            else selectedItemUse.text = "해제하시겠습니까?";
        }
        else if (selectedItem.item.itemType == ItemData.ItemType.Consume) 
            selectedItemUse.text = "사용하시겠습니까?";
    }

    private void ClearSelectedItemWindow()
    {
        selectedItem = null;

        selectedItemIcon.sprite = null;
        selectedItemName.text = string.Empty;
        selectedItemDesc.text = string.Empty;

        selectedItemStat.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        selectedItemUse.text = string.Empty;
    }

    public void OnConfirmButton()
    {
        if (selectedItem.item.itemType != ItemData.ItemType.Consume)
        {
            OnEquip();
        }
    }

    public void OnEquip()
    {
        int type = (int)selectedItem.item.itemType;

        if (!uiSlots[selectedItemIndex].equipped)
        {
            if (Equipment.Instance.slots[type].item == null)
            {
                Equipment.Instance.slots[type].item = selectedItem.item;
                equipIndex[type] = selectedItemIndex;

                uiSlots[selectedItemIndex].Equip(true);
                CalcStat(selectedItem.item, true);
            }
            else
            {
                uiSlots[equipIndex[type]].Equip(false);
                CalcStat(Equipment.Instance.slots[type].item, false);

                Equipment.Instance.slots[type].item = selectedItem.item;
                equipIndex[type] = selectedItemIndex;

                uiSlots[selectedItemIndex].Equip(true);
                CalcStat(selectedItem.item, true);
            }
        }
        else
        {
            uiSlots[equipIndex[type]].Equip(false);
            CalcStat(Equipment.Instance.slots[type].item, false);
            Equipment.Instance.slots[type].item = null;
        }
    }

    public void CalcStat(ItemData item, bool flag)
    {
        int type = (int)item.itemType;

        if (type == 0)
        {
            GameManager.Instance.Player.status.PlusAttack += flag ? item.baseAttack : item.baseAttack * -1;
        }
        else if (type == 1 || type == 2 || type == 3 || type == 4)
        {
            GameManager.Instance.Player.status.PlusDefense += flag ? item.baseDefense : item.baseDefense * -1;
            GameManager.Instance.Player.status.PlustMaxHealth += flag ? item.baseHealth : item.baseHealth * -1;
        }
        else if (type == 5)
        {
            GameManager.Instance.Player.status.PlustCritical += flag ? item.baseCritical : item.baseCritical * -1;
        }
    }
}
