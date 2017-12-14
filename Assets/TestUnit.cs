using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
//이벤트
public partial class TestUnit : MonoBehaviour
{
    public delegate void NormalEvent();

    public delegate void UnitEvent(TestUnit unit);
    public delegate void UnitFloatEvent(TestUnit unit, float amount);
    public delegate void UnitUnitEvent(TestUnit unit1, TestUnit unit2);

    public delegate void UnitEffectEvent(TestUnit unit, Effect effect);
    public delegate void UnitBuffEvent(TestUnit unit, Buff buff);

    public delegate void UnitHealEvent(TestUnit unit1, Heal damage);
    public delegate void UnitDamageEvent(TestUnit unit1,Damage damage);

    public delegate void UnitProjectileEvent(TestUnit unit, Projectile projectile);

    public delegate void UnitBlockEvent(TestUnit unit, Block block);

    public event UnitEvent BirthEvent;
    public void OnBirth()
    {
        BirthEvent(this);
    }
    public event UnitEvent DeadEvent;
    public void OnDeath()
    {
        DeadEvent(this);
    }
    public event UnitEvent Rebirth;
    public void OnRebirth()
    {
        Rebirth(this);
    }
    public event UnitFloatEvent MoveEvent;
    public void OnMove(float moveDistance)
    {
        MoveEvent(this, moveDistance);
    }

    public event UnitEvent EntangleEvent;
    public void OnEntangle()
    {
        EntangleEvent(this);
    }
    public event UnitEvent EntangleEndEvent;
    public void OnEntangleEnd()
    {
        EntangleEndEvent(this);
    }
    public event UnitEvent GroggyEvent;
    public void OnGroggy()
    {
        GroggyEvent(this);
    }
    public event UnitEvent GoggyEndEvent;
    public void OnGrggyEnd()
    {
        GoggyEndEvent(this);
    }
    public event UnitEvent SilenceEvent;
    public void OnSilence()
    {
        SilenceEvent(this);
    }
    public event UnitEvent SilenceEndEvent;
    public void OnSilenceEnd()
    {
        SilenceEndEvent(this);
    }

    public event UnitBuffEvent GetBuffEvent;
    public void OnBuff(Buff buff)
    {
        GetBuffEvent(this, buff);
    }
    public event UnitBuffEvent GetDebuffEvent;
    public void OnDebuff(Buff buff)
    {
        GetDebuffEvent(this, buff);
    }

    public event UnitEvent CastingEvent;
    public void OnCast()
    {
        CastingEvent(this);
    }

    public event UnitEvent AttackEvent;
    public void OnAttack()
    {
        AttackEvent(this);
    }
    public event UnitUnitEvent HitEvent;
    public void OnHit(TestUnit unit)
    {
        HitEvent(this, unit);
    }
    public event UnitEvent HitFailEvent;
    public void OnMiss()
    {
        HitFailEvent(this);
    }
    public event UnitDamageEvent DamageEvent;
    public void OnDamage(Damage damage)
    {
        DamageEvent(this, damage);
    }

    public event UnitProjectileEvent ProjectileCollisionEvent;
    public void OnCollisionProjectile(Projectile projectile)
    {
        ProjectileCollisionEvent(this, projectile);
    }

    public event UnitDamageEvent GetHitEvent;
    public void OnHitted(Effect effect)
    {
        //피격시 나타나는 효과들 재정리 할것.
        //이외 회피효과들도.
    }
    public event UnitDamageEvent GetDamageEvent;
    public void OnDamaged(Damage damage)
    {
        GetDamageEvent(this, damage);
    }

    public event UnitEffectEvent EvadeEvent;
    public void OnEvade(Effect effect)
    {
        EvadeEvent(this, effect);
    }
    public event UnitEffectEvent EvadeFailEvent;
    public void OnEvadeFail(Effect effect)
    {
        EvadeFailEvent(this, effect);
    }

    public event UnitHealEvent HealedEvent;
    public void OnHealedEvent(Heal heal)
    {
        HealedEvent(this, heal);
    }
    public event UnitHealEvent HealEvent;
    public void OnHealEvent(Heal heal)
    {
        HealedEvent(this, heal);
    }

    public event UnitBlockEvent UseAnyBlockEvent;
    public void OnUseAnyBlock(Block block)
    {
        UseAnyBlockEvent(this, block);
    }
    public event UnitBlockEvent UseNormalBlockEvent;
    public void OnUseNormalBlock(Block block)
    {
        UseNormalBlockEvent(this, block);
    }
    public event UnitBlockEvent UseSpecialBlockEvent;
    public void OnUseSpecialBlock(Block block)
    {
        UseSpecialBlockEvent(this, block);
    }

    public event UnitBlockEvent Use1BlockEvent;
    public void OnUse1Block(Block block)
    {
        Use1BlockEvent(this, block);
    }
    public event UnitBlockEvent Use2BlockEvent;
    public void OnUse2Block(Block block)
    {
        Use2BlockEvent(this, block);
    }
    public event UnitBlockEvent Use3BlockEvent;
    public void OnUse3Block(Block block)
    {
        Use3BlockEvent(this, block);
    }

    public event UnitBlockEvent ReactAnyBlockEvent;
    public void OnReactAnyBLock(Block block)
    {
        ReactAnyBlockEvent(this, block);
    }
    public event UnitBlockEvent React1BlockEvent;
    public void OnReact1BLock(Block block)
    {
        React1BlockEvent(this, block);
    }
    public event UnitBlockEvent React2BlockEvent;
    public void OnReact2BLock(Block block)
    {
        React2BlockEvent(this, block);
    }
    public event UnitBlockEvent React3BlockEvent;
    public void OnReact3BLock(Block block)
    {
        React3BlockEvent(this, block);
    }
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
        //TSET
        if(Input.GetKeyDown(KeyCode.X))
        {
            
        }
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
        //if (onhitevent != null)
        //{
        //    onhitevent.invoke(ref resultdamage);
        //}

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
public partial class TestUnit : MonoBehaviour
{

}