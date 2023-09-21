using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int level = 1;
    public int Level
    {
        get
        {
            return level;
        }
    }
    [SerializeField] private int exp = 0;
    public int Exp
    {
        get
        {
            return exp;
        }
    }
    [SerializeField] private int maxExp = 100;
    public int MaxExp
    {
        get
        {
            return maxExp;
        }
    }
    [SerializeField] private int gold = 1000;
    public int Gold
    {
        get
        {
            return gold;
        }
    }

    public Status status;

    void Start()
    {
        status = new Status(10, 5, 100, 0);
    }

    public void UpdateExp(int gain)
    {
        if (exp + gain < maxExp) exp += gain;
        else
        {
            level++;
            exp = maxExp - (exp + gain);
            maxExp += 10;
        }
    }
}
