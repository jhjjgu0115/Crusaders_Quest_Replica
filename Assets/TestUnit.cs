using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public partial class TestUnit : MonoBehaviour
{
    public string id = string.Empty;
    public string heroName = string.Empty;
    public E_HeroClass heroClass = E_HeroClass.None;
    public int level = 0;
    public E_GroupTag groupTag;

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
    //public Weapon weapon;
    //public Ring ring;

    //스킬 리스트 아니면 스킬사전
}
//상태
public partial class TestUnit : MonoBehaviour
{
    
}
//행동제어
public partial class TestUnit : MonoBehaviour
{
    Vector3 direction;
    public Transform targetPosition;
    void MovePosition(Vector3 targetPosition)
    {
        if (direction.x >= 0)
        {
            if (targetPosition.x > transform.position.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, statManager.CreateOrGetStat(E_StatType.MoveSpeed).ModifiedValue * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, statManager.CreateOrGetStat(E_StatType.MoveSpeed).ModifiedValue * Time.deltaTime * 0.5f);
            }
        }
        else
        {
            if (targetPosition.x < transform.position.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, statManager.CreateOrGetStat(E_StatType.MoveSpeed).ModifiedValue * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, statManager.CreateOrGetStat(E_StatType.MoveSpeed).ModifiedValue * Time.deltaTime * 0.5f);
            }
        }
    }
    Coroutine runningCoroutine;
    IEnumerator Moving()
    {
        while (true)
        {
            if (CanMove)
            {
                if (targetPosition)
                {
                    MovePosition(new Vector3(targetPosition.position.x, transform.position.y));
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
    bool canMove;
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
    public void StartMove()
    {
        runningCoroutine = StartCoroutine(Moving());
    }
    public void StopMove()
    {
        StopCoroutine(runningCoroutine);
    }




    public void StartRunning()
    {
        runningCoroutine = StartCoroutine(Moving());
    }
    public void StopRunning()
    {
        StopCoroutine(runningCoroutine);
    }
    
}
//스킬 관련
public partial class TestUnit : MonoBehaviour
{
    Dictionary<string, Skill> skillDict = new Dictionary<string, Skill>();
    IEnumerator BaseAttackCoolDown()
    {
        while(true)
        {
            yield return null;
        }
    }
}
//유니티 기본 메서드
public partial class TestUnit : MonoBehaviour
{
    private void Start()
    {
        direction = transform.worldToLocalMatrix.MultiplyVector(transform.right);
        StartCoroutine(Moving());

        statManager.CreateOrGetStat(E_StatType.CurrentHealth).AddEvent(HealthCheck);
    }
    IEnumerator TestEnumerator()
    {
        while(true)
        {
            float a = 1000;
            GetNormalDamage(ref a, E_DamageType.Physics, 550);
            yield return new WaitForSeconds(3);
        }
    }

    private void Update()
    {
        
    }

}
public partial class TestUnit : MonoBehaviour
{
    public void DebugLog()
    {
        Debug.Log("--------" + name + "--------");
        foreach (E_StatType stat in Enum.GetValues(typeof(E_StatType)))
        {
            Debug.Log(statManager.Get_Stat(stat).StatName + " : " + statManager.Get_Stat(stat).BaseValue);
        }
    }
    
}
public partial class TestUnit : MonoBehaviour
{
    /*
     * 피해를 받음
     * 피해 처리
     * 피해 이벤트
     * 
     * 회복을 받음
     * 회복 처리
     * 회복 이벤트
     * 
     * 스킬을 시전함
     * 스킬 이벤트
     * 스킬 처리
     * 
     * 회피함
     * 회피처리
     * 회피이벤트
     * 
     * 공격이 적중함
     * 적중 처리
     * 적중 이벤트
     * 
     */
    public delegate void DamageEvent(ref float damage);
    public DamageEvent OnHitEvent;



    public void GetNormalDamage(ref float damage, E_DamageType damageType, float penetrationPower)
    {
        float resultDamage = damage;
        float damageReducePercent = 0;
        E_FloatingType floatingType = E_FloatingType.NonpenetratingDamage;

        if (penetrationPower >= StatManager.CreateOrGetStat((E_StatType)damageType).ModifiedValue)
        {
            damageReducePercent = 1;//1이면 풀관통
            floatingType = E_FloatingType.FullPenetrationDamage;
        }
        else
        {
            damageReducePercent = (100 / (((StatManager.CreateOrGetStat((E_StatType)damageType).ModifiedValue - penetrationPower) * 0.348f) + 100));
            if (damageReducePercent > 0.85f)
            {
                floatingType = E_FloatingType.FullPenetrationDamage;
            }
        }
        if (OnHitEvent != null)
        {
            OnHitEvent.Invoke(ref resultDamage);
        }

        resultDamage *= (damageReducePercent * (1 - StatManager.CreateOrGetStat(E_StatType.DamageReduceRate).ModifiedValue));
        StatManager.CreateOrGetStat(E_StatType.CurrentHealth).ModifiedValue -= resultDamage;
        FloatingNumberManager.FloatingNumber(gameObject, resultDamage, floatingType);
    }
    public void GetCriticalDamage(ref float damage, E_DamageType damageType, float penetrationPower)
    {
        float resultDamage = damage;
        float damageReducePercent = 0;
        if (penetrationPower >= StatManager.CreateOrGetStat((E_StatType)damageType).ModifiedValue)
        {
            damageReducePercent = 1;//1이면 풀관통
        }
        else
        {
            damageReducePercent = (100 / (((StatManager.CreateOrGetStat((E_StatType)damageType).ModifiedValue - penetrationPower) * 0.348f) + 100));
        }
        resultDamage *= damageReducePercent;
        resultDamage *= (1 - StatManager.CreateOrGetStat(E_StatType.DamageReduceRate).ModifiedValue);
        StatManager.CreateOrGetStat(E_StatType.CurrentHealth).ModifiedValue -= resultDamage;
        FloatingNumberManager.FloatingNumber(gameObject, resultDamage, E_FloatingType.CriticalDamage);
    }

    public void GetHeal()
    {

    }

    public bool Evade()
    {
        return true;
    }
    public bool OnHit()
    {
        return true;
    }

    public void SkillUse()
    {

    }

}
public partial class TestUnit : MonoBehaviour
{
}
public partial class TestUnit : MonoBehaviour
{
}
//이벤트
public partial class TestUnit : MonoBehaviour
{
    public delegate void UnitEvent(TestUnit unit);
    public event UnitEvent DeadEvent;
    void HealthCheck(float health)
    {
        if (health<=0)
        {
            Dead();
        }
    }

    void Dead()
    {
        if (DeadEvent != null)
        {
            DeadEvent(this);
        }
    }
}