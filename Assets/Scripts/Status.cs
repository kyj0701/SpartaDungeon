using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    private int attack;
    public int Attack
    {
        get
        {
            return attack;
        }
        set
        {
            attack = value;
        }
    }

    private int defense;
    public int Defense
    {
        get
        {
            return defense;
        }
        set
        {
            defense = value;
        }
    }

    private int maxHealth;
    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            maxHealth = value;
        }
    }

    private int critical;
    public int Critical
    {
        get
        {
            return critical;
        }
        set
        {
            critical = value;
        }
    }

    private int plusAttack = 0;
    public int PlusAttack
    {
        get
        {
            return plusAttack;
        }
        set
        {
            plusAttack = value;
        }
    }

    private int plusDefense = 0;
    public int PlusDefense
    {
        get
        {
            return plusDefense;
        }
        set
        {
            plusDefense = value;
        }
    }

    private int plusMaxHealth = 0;
    public int PlustMaxHealth
    {
        get
        {
            return plusMaxHealth;
        }
        set
        {
            plusMaxHealth = value;
        }
    }

    private int plusCritical = 0;
    public int PlustCritical
    {
        get
        {
            return plusCritical;
        }
        set
        {
            plusCritical = value;
        }
    }

    public Status(int _attack, int _defense, int _maxHealth, int _critical)
    {
        attack = _attack;
        defense = _defense;
        maxHealth = _maxHealth;
        critical = _critical;
    }
}
