using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unit : MonoBehaviour
{

    StatManager statManager = new StatManager();
    BuffManager buffManager = new BuffManager();

    public StatManager StatManager
    {
        get
        {
            return statManager;
        }
    }
    public BuffManager BuffManager
    {
        get
        {
            return buffManager;
        }
    }

    public Queue<string> actionQueue = new Queue<string>();
    public Queue<Skill> skillQueue = new Queue<Skill>();

    private void Start()
    {
        StartCoroutine(AutoAction());
    }


    IEnumerator AutoAction()
    {
        Animator animator = GetComponent<Animator>();
        while(true)
        {
            if (actionQueue.Count!=0)
            {
                string curruntAnim = actionQueue.Dequeue();
                animator.Play(curruntAnim);
                yield return null;
                while (animator.GetCurrentAnimatorStateInfo(0).IsName(curruntAnim))
                {
                    Debug.Log(curruntAnim);
                    yield return null;
                }
                Debug.Log(curruntAnim + "done!");
            }
            yield return null;
        }
    }











    public void ShowAllStat()
    {
        foreach (E_StatType _statType in Enum.GetValues(typeof(E_StatType)))
        {
            Debug.Log(statManager.Get_Stat(_statType).StatName + "/" + statManager.Get_Stat(_statType).StatType + "/" + statManager.Get_Stat(_statType).ModifiedValue);
        }
    }
}
