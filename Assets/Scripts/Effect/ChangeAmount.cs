using UnityEngine;
using System.Collections.Generic;

public class ChangeAmount : Effect
{
    public E_MultiplierType multiplierType;

    public float changeAmountMultiplier;

    public override void ActivateEffect(Unit caster, Unit target, ref float amount, float multiplier)
    {
        if(ConditionCheck())
        {
            switch (multiplierType)
            {
                case E_MultiplierType.Add:
                    amount *= (changeAmountMultiplier + multiplier);
                    break;

                case E_MultiplierType.Square:
                    amount *= Mathf.Pow(changeAmountMultiplier, multiplier);
                    break;

                case E_MultiplierType.DirectMultiply:
                    amount *= changeAmountMultiplier * multiplier;
                    break;
                default:
                    break;
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
