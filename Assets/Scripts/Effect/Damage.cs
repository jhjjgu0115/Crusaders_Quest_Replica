using UnityEngine;
using System.Collections.Generic;

public class Damage : Effect
{
    public List<AmountSet> damageList;
    public float totalAmount = 0;
    public float fixedAmount = 0;
    public float targetBasedAmount = 0;
    public float casterBasedAmount = 0;

    public override void RefreshAllAmount(Unit caster, Unit target)
    {
        totalAmount = 0;
        fixedAmount = 0;
        casterBasedAmount = 0;
        targetBasedAmount = 0;
        for (int index = 0; index < damageList.Count; index++)
        {
            fixedAmount += damageList[index].FixedAmount;
            casterBasedAmount += damageList[index].SetAmountCasterBased(caster);
            targetBasedAmount += damageList[index].SetAmountTargetBased(target);
        }
        totalAmount = fixedAmount + casterBasedAmount + targetBasedAmount;
    }
    public override void RefreshFixedAllAmount()
    {
        fixedAmount = 0;
        for (int index = 0; index < damageList.Count; index++)
        {
            fixedAmount += damageList[index].FixedAmount;
        }
        totalAmount = fixedAmount + targetBasedAmount + casterBasedAmount;
    }
    public override void RefreshTargetBasedAmount(Unit target)
    {
        totalAmount = 0;
        fixedAmount = 0;
        targetBasedAmount = 0;
        for (int index = 0; index < damageList.Count; index++)
        {
            fixedAmount += damageList[index].FixedAmount;
            targetBasedAmount += damageList[index].SetAmountTargetBased(target);
        }
        totalAmount = fixedAmount + targetBasedAmount + casterBasedAmount;
    }
    public override void RefreshCasterBasedAmount(Unit caster)
    {
        totalAmount = 0;
        fixedAmount = 0;
        casterBasedAmount = 0;
        for (int index = 0; index < damageList.Count; index++)
        {
            fixedAmount += damageList[index].FixedAmount;
            casterBasedAmount += damageList[index].SetAmountCasterBased(caster);
        }
        totalAmount = fixedAmount + targetBasedAmount + casterBasedAmount;
    }
    public override void ActivateEffect(Unit caster, Unit target, float multiplier)
    {
        if(ConditionCheck())
        {
            target.GetDamaged(totalAmount * multiplier);
        }
    }

    protected override bool ConditionCheck()
    {
        for (int index = 0; index < validatorList.Count; index++)
        {
            if (!validatorList[index].Check(caster, target))
            {
                return false;
            }
        }
        return true;
    }
}
