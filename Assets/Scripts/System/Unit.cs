using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Animator animator;

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


    /// <summary>
    /// 사망 여부
    /// </summary>
    public bool isDead = false;
    public bool isGroggy = false;
    public bool inBattle = false;
    public bool isEnemyInRange = false;
    public bool canMove = true;

    public bool isNormalState = true;




    private void Start()
    {
        animator = GetComponent<Animator>();
        StartMoveForward();
    }



    /// <summary>
    /// 전진 시작
    /// </summary>
    public void StartMoveForward()
    {
        moveCoroutine=StartCoroutine(MoveForward());
    }
    Coroutine moveCoroutine;
    IEnumerator MoveForward()
    {
        StatFloat moveSpeed = StatManager.CreateOrGetStat(E_StatType.MoveSpeed);
        while(true)
        {
            Debug.Log(GetComponent<Rigidbody2D>().velocity);
            transform.Translate(Time.deltaTime*moveSpeed.ModifiedValue,0,0);
            yield return null;
        }

    }



    public bool isAttackCoolDownState = false;
    public void Attack()
    {
        animator.Play("Attack1");
        isAttackCoolDownState = true;
        StartCoroutine(AttackCoolDown());
    }
    
    IEnumerator AttackCoolDown()
    {
        float coolTime = StatManager.CreateOrGetStat(E_StatType.AttackSpeed).ModifiedValue;
        float remainCoolTime = 0;
        while (true)
        {
            if(coolTime> remainCoolTime)
            {
                remainCoolTime += Time.deltaTime;
            }
            else
            {
                isAttackCoolDownState = false;
                StartCoroutine(TryAttack());
                break;
            }
            yield return null;
        }
    }
    IEnumerator TryAttack()
    {
        while (true)
        {
            if(!isAttackCoolDownState)//기본공격 가능상태
            {
                if(isEnemyInRange)//사거리내 적이 있음
                {
                    if(isNormalState)//캐릭터가 정상
                    {
                        Attack();//기본 공격 개시
                    }
                }
            }
            yield return null;
        }
    }
    Coroutine KnockBackCoroutine;
    bool isKnockBackRunning = false;
    /// <summary>
    /// 넉백
    /// </summary>
    /// <param name="_power"></param>
    public void GiveKnockBack(float _power)
    {
        GetComponent<Rigidbody2D>().AddForce(-transform.right * _power*(10-statManager.CreateOrGetStat(E_StatType.KnockbackResistance).ModifiedValue), ForceMode2D.Impulse);
        if(!isKnockBackRunning)
        {
            if(((-transform.right * _power * (10 - statManager.CreateOrGetStat(E_StatType.KnockbackResistance).ModifiedValue)).x < -1.0f))
            {
                StopCoroutine(moveCoroutine);
                isKnockBackRunning = true;
                KnockBackCoroutine = StartCoroutine(KnockBackEndCheck());
            }
        }
        
    }
    IEnumerator KnockBackEndCheck()
    {
        Rigidbody2D rig = GetComponent<Rigidbody2D>();
        while (rig.velocity.x<-1)
        {
            yield return null;
        }
        rig.velocity = Vector2.zero;
        isKnockBackRunning = false;
        StartMoveForward();
    }











    //기본 행위자.
    //전진
    //후진
    //대기

    //기본 공격
    //스킬 시전




















    bool canForward = true;

    bool isOverRange = false;
    IEnumerator MoveBackWard()
    {
        Rigidbody2D rig = GetComponent<Rigidbody2D>();
        while (true)
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
