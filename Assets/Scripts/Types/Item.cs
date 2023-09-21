using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{ 
    public enum ItemType { Weapon, Armor, Helmet, Shoes, Accessory, Consume }

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public int baseAttack;
    public int baseDefense;
    public int baseHealth;
    public int baseCritical;
    public int upgrade;
    public float[] upgradeAttacks;
    public float[] upgradeHealthes;
    public float[] upgradeDefenses;
    public float[] upgradeCriticals;

    [Header("# Stacking")]
    public bool canStack;
    public int maxStackAmount;

    public Item (int _itemId, string _itemName, string _itemDesc, Sprite _itemIcon, int _baseAttack, int _baseDefense, int _baseHealth, int _baseCritical, int _upgrade, float[] _upgradeAttacks,  float[] _upgradeDefenses, float[] _upgradeHealthes, float[] _upgradeCriticals, bool _canStack, int _maxStackAmount)
    {
        itemId = _itemId;
        itemName = _itemName;
        itemDesc = _itemDesc;
        itemIcon = _itemIcon;
        baseAttack = _baseAttack;
        baseDefense = _baseDefense;
        baseHealth = _baseHealth;
        baseCritical = _baseCritical;
        upgrade = _upgrade;
        upgradeAttacks = _upgradeAttacks;
        upgradeDefenses = _upgradeDefenses;
        upgradeHealthes = _upgradeHealthes;
        upgradeCriticals = _upgradeCriticals;
        canStack = _canStack;
        maxStackAmount = _maxStackAmount;
    }
}
