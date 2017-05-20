using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Unit player;
    public Unit enemy;



	// Use this for initialization
	void Start ()
    {
        foreach (E_StatType _statType in Enum.GetValues(typeof(E_StatType)))
        {
            player.StatManager.Create_Stat(_statType,0);
        }
        foreach (E_StatType _statType in Enum.GetValues(typeof(E_StatType)))
        {
            enemy.StatManager.Create_Stat(_statType, 0);
        }


        player.ShowAllStat();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
