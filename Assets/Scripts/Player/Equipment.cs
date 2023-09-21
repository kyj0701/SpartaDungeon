using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot
{
    public ItemData item; // 0: Weapon, 1: Armor, 2: Helmet, 3: Shoes, 4: Accessary
}

public class Equipment : MonoBehaviour
{
    private static Equipment instance = null;
    public static Equipment Instance
    {
        get
        {
            return instance == null ? null : instance;
        }
    }

    public EquipSlot[] slots;

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
        slots = new EquipSlot[5];

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = new EquipSlot();
        }
    }
}
