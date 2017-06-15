using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLaunchEffect : Effect
{
    public float launchAngle = 0;
    public float launchVelocity = 0;
    public Vector3 launchOffset;

    public Projectile projectile;
    public bool isLockOn = false;
    
    public override void ActivateEffect()
    {

    }
    public override void ActivateEffect(Unit caster, Unit targetList)
    {
    }
    public override void ActivateEffect(Unit caster, Unit targetList, float multiplier)
    {
    }
    public override void ActivateEffect(Unit caster, Unit targetList, ref float amount, float multiplier)
    {
        Projectile instanceProjectile = Instantiate(projectile);

        //instanceProjectile.Initialize(caster, target, multiplier);
        //instanceProjectile.FlyingStart();
    }

    public override void RefreshAllAmount(Unit caster, Unit targetList)
    {
    }
    public override void RefreshTargetBasedAmount(Unit targetList)
    {
    }
    public override void RefreshCasterBasedAmount(Unit caster)
    {
    }
    public override void RefreshFixedAllAmount()
    {

    }


    public override bool ConditionCheck()
    {/*
        for(int index=0; index < validatorList.Count; index++)
        {
            if(!validatorList[index].Check(caster, target))
            {
                return false;
            }
        }
        return true;*/
        return true;
    }
}
