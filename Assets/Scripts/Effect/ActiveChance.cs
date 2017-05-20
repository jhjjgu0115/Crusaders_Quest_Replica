using UnityEngine;
using System.Collections.Generic;

public class ActiveChance : Effect {
    public float percentage = 1;
    public List<Effect> effectList;

    public override void RefreshAllAmount(Unit caster, List<Unit> target)
    {
        for (int index = 0; index < effectList.Count; index++)
        {
            effectList[index].RefreshAllAmount(caster, target);
        }
    }
    public override void RefreshFixedAllAmount()
    {
        for (int index = 0; index < effectList.Count; index++)
        {
            effectList[index].RefreshFixedAllAmount();
        }
    }
    public override void RefreshTargetBasedAmount(List<Unit> target)
    {
        for (int index = 0; index < effectList.Count; index++)
        {
            effectList[index].RefreshTargetBasedAmount(target);
        }
    }
    public override void RefreshCasterBasedAmount(Unit caster)
    {
        for (int index = 0; index < effectList.Count; index++)
        {
            effectList[index].RefreshCasterBasedAmount(caster);
        }
    }

    public override void ActivateEffect(Unit caster, List<Unit> target, float multiplier)
    {
        if(ConditionCheck())
        {
            if (percentage >= Random.value)
            {
                for (int index = 0; index < effectList.Count; index++)
                {
                    effectList[index].ActivateEffect(caster, target, multiplier);
                }
            }
        }
    }


    protected override bool ConditionCheck()
    {
        /*for(int index=0; index < validatorList.Count; index++)
        {
            if(!validatorList[index].Check(caster, target))
            {
                return false;
            }
        }*/
        return true;
    }


}
