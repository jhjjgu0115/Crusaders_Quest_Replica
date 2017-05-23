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

        StartCoroutine(TestAction());
    }
	

    IEnumerator TestAction()
    {
        yield return new WaitForSeconds(0.5f);
        player.actionQueue.Enqueue("Attack1");
        player.actionQueue.Enqueue("Uria_Skill1");
        player.actionQueue.Enqueue("Uria_Skill1");
        player.actionQueue.Enqueue("Uria_Skill1");
        player.actionQueue.Enqueue("Uria_Skill1");
        player.actionQueue.Enqueue("Uria_Skill1");
        player.actionQueue.Enqueue("Attack1");
        yield return new WaitForSeconds(0.5f);
        player.actionQueue.Enqueue("Attack1");
        yield return new WaitForSeconds(0.5f);
        player.actionQueue.Enqueue("Attack1");
        yield return new WaitForSeconds(0.5f);
        player.actionQueue.Enqueue("Attack1");
        yield return new WaitForSeconds(0.5f);
        player.actionQueue.Enqueue("Attack1");
        yield return new WaitForSeconds(0.5f);
        player.actionQueue.Enqueue("Attack1");
        yield return null;
    }
}
