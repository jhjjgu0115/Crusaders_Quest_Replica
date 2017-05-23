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


    private void Start()
    {
        StartCoroutine(AutoAction());
        StartCoroutine(MoveForward());
    }
    //기본 행위자.
    //전진
    //후진
    //대기

    //기본 공격
    //스킬 시전




















    bool canForward = true;
    IEnumerator MoveForward()
    {
        Rigidbody2D rig = GetComponent<Rigidbody2D>();
        while (canForward)
        {
            rig.AddForce(transform.right * 3,ForceMode2D.Force);
            yield return null;
        }
    }
    bool isOverRange = false;
    IEnumerator MoveBackWard()
    {
        Rigidbody2D rig = GetComponent<Rigidbody2D>();
        while (isOverRange)
        {
            rig.AddForce(-transform.right * 10, ForceMode2D.Impulse);
            yield return new WaitForSeconds(3.0f);
        }
    }






    IEnumerator AutoAction()
    {
        Animator animator = GetComponent<Animator>();
        while(true)
        {
            /*if (actionQueue.Count!=0)
            {
                string curruntAnim = actionQueue.Dequeue();
                animator.Play(curruntAnim);
                yield return null;
                while (animator.GetCurrentAnimatorStateInfo(0).IsName(curruntAnim))
                {
                    yield return null;
                }
                Debug.Log(curruntAnim + "done!");
            }*/
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
