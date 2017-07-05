using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DamageEffect : Effect
{
    public E_DamageType damageType=E_DamageType.Physics;
    
    public List<AmountSet> damageList;
    public float totalDamage;
    public float fixedDamage;
    public float casterBasedDamage;
    public float targetBasedDamage;

    
    public List<AmountSet> penetrationList;
    public float totalPenetration;
    public float fixedPenetration;
    public float casterBasedPenetration;
    public float targetBasedPenetration;





    public override void RefreshAllAmount(Unit caster, Unit target)
    {
        totalDamage = 0;
        fixedDamage = 0;
        casterBasedDamage = 0;
        targetBasedDamage = 0;
        for (int index = 0; index < damageList.Count; index++)
        {
            fixedDamage += damageList[index].FixedAmount;
            casterBasedDamage += damageList[index].SetAmountCasterBased(caster);
            targetBasedDamage += damageList[index].SetAmountTargetBased(target);
        }
        totalDamage = fixedDamage + casterBasedDamage + targetBasedDamage;

        totalPenetration = 0;
        fixedPenetration = 0;
        casterBasedPenetration = 0;
        targetBasedPenetration = 0;
        for (int index = 0; index < damageList.Count; index++)
        {
            fixedDamage += damageList[index].FixedAmount;
            casterBasedDamage += penetrationList[index].SetAmountCasterBased(caster);
            targetBasedDamage += penetrationList[index].SetAmountTargetBased(target);
        }
        totalPenetration = fixedPenetration + casterBasedPenetration + targetBasedPenetration;
    }
    public override void RefreshFixedAllAmount()
    {
        fixedDamage = 0;
        for (int index = 0; index < damageList.Count; index++)
        {
            fixedDamage += damageList[index].FixedAmount;
        }
        totalDamage = fixedDamage + targetBasedDamage + casterBasedDamage;

        fixedPenetration = 0;
        for (int index = 0; index < penetrationList.Count; index++)
        {
            fixedPenetration += penetrationList[index].FixedAmount;
        }
        totalPenetration = fixedPenetration + targetBasedPenetration + casterBasedPenetration;
    }
    public override void RefreshTargetBasedAmount(Unit target)
    {
        Debug.Log(target);
        totalDamage = 0;
        fixedDamage = 0;
        targetBasedDamage = 0;
        for (int index = 0; index < damageList.Count; index++)
        {
            fixedDamage += damageList[index].FixedAmount;
            targetBasedDamage += damageList[index].SetAmountTargetBased(target);
        }
        totalDamage = fixedDamage + casterBasedDamage + targetBasedDamage;

        totalPenetration = 0;
        fixedPenetration = 0;
        casterBasedPenetration = 0;
        targetBasedPenetration = 0;
        for (int index = 0; index < penetrationList.Count; index++)
        {
            fixedPenetration += penetrationList[index].FixedAmount;
            targetBasedPenetration += penetrationList[index].SetAmountTargetBased(target);
        }
        totalPenetration = fixedPenetration + casterBasedPenetration + targetBasedPenetration;
    }
    public override void RefreshCasterBasedAmount(Unit caster)
    {
        totalDamage = 0;
        fixedDamage = 0;
        casterBasedDamage = 0;
        for (int index = 0; index < damageList.Count; index++)
        {
            fixedDamage += damageList[index].FixedAmount;
            casterBasedDamage += damageList[index].SetAmountCasterBased(caster);
        }

        totalDamage = fixedDamage + casterBasedDamage + targetBasedDamage;

        totalPenetration = 0;
        fixedPenetration = 0;
        casterBasedPenetration = 0;
        for (int index = 0; index < penetrationList.Count; index++)
        {
            fixedPenetration += penetrationList[index].FixedAmount;
            casterBasedPenetration += penetrationList[index].SetAmountCasterBased(caster);
        }
        totalPenetration = fixedPenetration + casterBasedPenetration + targetBasedPenetration;
    }
    public override void ActivateEffect(Unit caster, Unit target, float multiplier)
    {
        if (true)
        {
            target.GetDamage(ref totalDamage, damageType, totalPenetration* multiplier);
        }
    }
    public override void ActivateEffect(Unit caster, Unit target)
    {
        if (true)
        {
            target.GetDamage(ref totalDamage,damageType,totalPenetration);
        }
    }

    /* protected override bool ConditionCheck()
     {
         for (int index = 0; index < validatorList.Count; index++)
         {
             if (!validatorList[index].Check(caster, target))
             {
                 return false;
             }
         }
         return true;
     }*/
}
