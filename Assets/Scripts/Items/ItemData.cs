using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Weapon, Shield, Armor, Helmet, Shoes, Accessory, Consume }

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
    public int upgrade = 0;
    public float[] upgradeAttacks;
    public float[] upgradeHealthes;
    public float[] upgradeDefenses;
    public float[] upgradeCriticals;

    [Header("# Stacking")]
    public bool canStack;
    public int maxStackAmount;
}
