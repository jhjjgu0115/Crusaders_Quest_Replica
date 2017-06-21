using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEffect : Effect
{
    public List<Effect> effectList = new List<Effect>();

    public override void ActivateEffect()
    {
        base.ActivateEffect();
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
}
