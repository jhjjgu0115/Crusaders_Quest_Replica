using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : Effect
{
    public override void ActivateEffect()
    {
        
    }
    public override void ActivateEffect(Unit caster)
    {
        
    }
    public override void ActivateEffect(Unit caster, Unit target)
    {
    }

    public override void ActivateEffect(Unit caster, Unit target, float multiplier)
    {
    }
    public override void ActivateEffect(Unit caster, Unit target, ref float amount, float multiplier)
    {
    }


    public override void RefreshAllAmount(Unit caster, Unit target)
    {
    }
    public override void RefreshCasterBasedAmount(Unit caster)
    {
    }
    public override void RefreshTargetBasedAmount(Unit target)
    {
    }
    public override void RefreshFixedAllAmount()
    {
    }


    public override bool ConditionCheck()
    {
        return true;
    }

}
