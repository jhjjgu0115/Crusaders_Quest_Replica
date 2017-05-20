using UnityEngine;
using System.Collections.Generic;

public class StatChange : Effect
{
    bool setFlag = false; //true = 적용, false = 미적용

    public List<AmountSet> changeAmountList;
    public float totalAmount = 0;
    public float fixedAmount = 0;
    public float targetBasedAmount = 0;
    public float casterBasedAmount = 0;

    public E_StatType targetStat;

    public override void RefreshAllAmount(Unit caster, Unit target)
    {
        if (!setFlag)
        {
            totalAmount = 0;
            fixedAmount = 0;
            casterBasedAmount = 0;
            targetBasedAmount = 0;
            for (int index = 0; index < changeAmountList.Count; index++)
            {
                fixedAmount += changeAmountList[index].FixedAmount;
                casterBasedAmount += changeAmountList[index].SetAmountCasterBased(caster);
                targetBasedAmount += changeAmountList[index].SetAmountTargetBased(target);
            }
            totalAmount = fixedAmount + casterBasedAmount + targetBasedAmount;
        }
    }
    public override void RefreshFixedAllAmount()
    {
        if (!setFlag)
        {
            fixedAmount = 0;
            for (int index = 0; index < changeAmountList.Count; index++)
            {
                fixedAmount += changeAmountList[index].FixedAmount;
            }
            totalAmount = fixedAmount + targetBasedAmount + casterBasedAmount;
        }
    }
    public override void RefreshTargetBasedAmount(Unit target)
    {
        if (!setFlag)
        {
            totalAmount = 0;
            fixedAmount = 0;
            targetBasedAmount = 0;
            for (int index = 0; index < changeAmountList.Count; index++)
            {
                fixedAmount += changeAmountList[index].FixedAmount;
                targetBasedAmount += changeAmountList[index].SetAmountTargetBased(target);
            }
            totalAmount = fixedAmount + targetBasedAmount + casterBasedAmount;
        }

    }
    public override void RefreshCasterBasedAmount(Unit caster)
    {
        if (!setFlag)
        {
            totalAmount = 0;
            fixedAmount = 0;
            casterBasedAmount = 0;
            for (int index = 0; index < changeAmountList.Count; index++)
            {
                fixedAmount += changeAmountList[index].FixedAmount;
                casterBasedAmount += changeAmountList[index].SetAmountCasterBased(caster);
            }
            totalAmount = fixedAmount + targetBasedAmount + casterBasedAmount;
        }
    }
    public override void ActivateEffect(Unit caster, Unit target, float multiplier)
    {
        if (ConditionCheck())
        {
            if (setFlag == false)
            {
                setFlag = true;
                target.StatManager.CreateOrGetStat(targetStat).ModifiedValue += totalAmount;
            }
            else
            {
                setFlag = false;
                target.StatManager.CreateOrGetStat(targetStat).ModifiedValue -= totalAmount;
            }
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
