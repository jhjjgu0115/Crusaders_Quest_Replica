using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public partial class Unit : MonoBehaviour
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

    bool isDead;
    bool isGroggy;
    bool inBattle;
    bool isEnemyInRange;
    bool canMove;
    bool isNormalState;
    bool canInteraction;
    public Unit()
    {

    }


    /*

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
            }
            yield return null;
        }
    }









*/

  
}
public partial class Unit : MonoBehaviour
{
    public bool CanMove
    {
        get
        {
            return canMove;
        }
        set
        {
            canMove = value;
        }
    }
    public bool IsGroggy
    {
        get
        {
            return isGroggy;
        }
        set
        {
            isGroggy = value;
            animator.SetBool("isGroggy", value);
        }
    }
    public bool CanInteraction
    {
        get
        {
            return canInteraction;
        }
        set
        {
            canInteraction = value;
            if(value)
            {
                
            }
            else
            {
                //충돌체 off
            }
        }
    }
    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            isDead = value;
            animator.SetBool("isDead", value);
        }
    }
    public bool InBattle
    {
        get
        {
            return inBattle;
        }
        set
        {
            inBattle = value;
            animator.SetBool("inBattle", value);
        }
    }
    
    
    
    public void GroggyStart()
    {
        if(canInteraction)
        {
            IsGroggy = true;
            animator.SetTrigger("groggyTrigger");
        }
    }

    public void Kill()
    {
        if(canInteraction)
        {
            IsDead = true;
            CanInteraction = false;
            animator.SetTrigger("deadTrigger");
            statManager.CreateOrGetStat(E_StatType.CurrentHealth).ModifiedValue = 0;
        }
    }

    public void Revive(float value)
    {
        statManager.CreateOrGetStat(E_StatType.CurrentHealth).ModifiedValue = value;
        CanInteraction = true;
        IsDead = false;
    }
    Coroutine moveforward;
    public void RunForward()
    {
        moveforward = StartCoroutine(MoveForward());

    }
    IEnumerator MoveForward()
    {
        //기능 : 앞으로 전진한다.
        //조건 : 적이 최소사거리 내에 들어오기 전까지
        StatFloat moveSpeed = StatManager.CreateOrGetStat(E_StatType.MoveSpeed);
        while (canInteraction)
        {
            transform.Translate(Time.deltaTime * moveSpeed.ModifiedValue, 0, 0);
            yield return null;
        }
    }


    public void RunSteady()
    {
        //기능 : 제자리 달리가.
        //조건 : 적이 최소사거리 오차범위내

    }
    public void RunBackWard()
    {
        //기능 : 뒤로 천천히 후진
        //조건 : 적이 최소 사거리 안으로 들어옴
    }
    public void BasicAttack()
    {
        //기능: 적에게 기본 공격을 시전함.
        //조건: 적이 최대 사거리 안으로 들어옴.
        //      스킬 대기열이 비어있음.
    }

    IEnumerator RangeCheck()
    {
        while(true)
        {
            //여기서 레인지 체크
            yield return null;
        }
    }

    public void EnterTheBattle()
    {
        InBattle = true;

    }
    public void EscapeTheBattle()
    {
        InBattle = false;

    }



    

    /// <summary>
    /// 사망 조건 체크
    /// </summary>
    /// <param name="_HP"></param>
    void CheckHP(float _HP)
    {
        if (_HP > 0)
        {
        }
        else
        {
            if(canInteraction)
            {
                IsDead = false;
                CanInteraction = false;
                animator.SetTrigger("deadTrigger");
                statManager.CreateOrGetStat(E_StatType.CurrentHealth).ModifiedValue = 0;
            }
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        StatManager.CreateOrGetStat(E_StatType.CurrentHealth).AddEvent(CheckHP);

    }

}
//디버그
public partial class Unit : MonoBehaviour
{
    public void ShowAllStat()
    {
        foreach (E_StatType _statType in Enum.GetValues(typeof(E_StatType)))
        {
            Debug.Log(statManager.Get_Stat(_statType).StatName + "/" + statManager.Get_Stat(_statType).StatType + "/" + statManager.Get_Stat(_statType).ModifiedValue);
        }
    }
}
