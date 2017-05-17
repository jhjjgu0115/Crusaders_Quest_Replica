using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    protected string statName;
    protected EStatType statType;


    public EStatType StatType
    {
        get
        {
            return statType;
        }
    }
    public string StatName
    {
        get
        {
            return statName;
        }
    }
}
