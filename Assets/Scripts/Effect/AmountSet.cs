using UnityEngine;
using System.Collections;
[System.Serializable]
public class AmountSet
{
    public E_DamageType elementalType;

    float fixedAmount;
    public float FixedAmount
    {
        get
        {
            return fixedAmount;
        }
        set
        {
            fixedAmount = value;
        }
    }
    float casterStatBasedMultiplier;
    public float CasterStatBasedMultiplier
    {
        get
        {
            return casterStatBasedMultiplier;
        }
        set
        {
            casterStatBasedMultiplier = value;
        }
    }
    public E_StatType casterStatBasedType;
    float targetStatBasedMultiplier;
    public float TargetStatBasedMultiplier
    {
        get
        {
            return targetStatBasedMultiplier;
        }
        set
        {
            targetStatBasedMultiplier = value;
        }
    }
    public E_StatType targetStatBasedType;

    /// <summary>
    /// 시전자 기반 값 갱신
    /// </summary>
    /// <param name="caster"></param>
    /// <returns></returns>
    public float SetAmountCasterBased(Unit caster)
    {
        return casterStatBasedMultiplier * caster.StatManager.CreateOrGetStat(casterStatBasedType).ModifiedValue;
    }
    /// <summary>
    /// 대상 기반값 갱신
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public float SetAmountTargetBased(Unit target)
    {
        return targetStatBasedMultiplier * target.StatManager.CreateOrGetStat(targetStatBasedType).ModifiedValue;
    }

}
