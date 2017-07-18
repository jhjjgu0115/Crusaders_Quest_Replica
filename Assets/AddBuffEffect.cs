using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AddBuffEffect : Effect
{
    public List<Buff> buffList = new List<Buff>();

    public override void ActivateEffect()
    {
        base.ActivateEffect();
    }
    public override void ActivateEffect(Unit caster)
    {
        base.ActivateEffect(caster);
    }
    public override void ActivateEffect(Unit caster, Unit target)
    {
        base.ActivateEffect(caster, target);
    }
    public override void ActivateEffect(Unit caster, Unit target, float multiplier)
    {
        base.ActivateEffect(caster, target, multiplier);
    }
    public override void ActivateEffect(Unit caster, Unit target, ref float amount, float multiplier)
    {
        base.ActivateEffect(caster, target, ref amount, multiplier);
    }
    public override bool ConditionCheck()
    {
        return base.ConditionCheck();
    }

    public override void RefreshCasterBasedAmount(Unit caster)
    {
        base.RefreshCasterBasedAmount(caster);
    }
    public override void RefreshFixedAllAmount()
    {
        base.RefreshFixedAllAmount();
    }
    public override void RefreshTargetBasedAmount(Unit target)
    {
        base.RefreshTargetBasedAmount(target);
    }

    public override void RefreshAllAmount(Unit caster, Unit target)
    {
        base.RefreshAllAmount(caster, target);
    }
}
