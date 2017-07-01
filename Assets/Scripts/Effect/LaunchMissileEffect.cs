﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchMissileEffect : Effect
{
    public Vector3 launchAngle;
    public Vector3 launchVelocity;
    public GameObject launchPosition;
    public Vector3 offset;


    public Projectile projectile;
    public int launchCount=0;
    public float launchPeriod = 0;

    public override void RefreshAllAmount(Unit caster, Unit target)
    {
        foreach(Effect effect in projectile.destroyEffectList)
        {
            effect.RefreshAllAmount(caster, target);
        }
        foreach (Effect effect in projectile.impactEffectList)
        {
            effect.RefreshAllAmount(caster, target);
        }
    }
    public override void RefreshFixedAllAmount()
    {
        foreach (Effect effect in projectile.destroyEffectList)
        {
            effect.RefreshFixedAllAmount();
        }
        foreach (Effect effect in projectile.impactEffectList)
        {
            effect.RefreshFixedAllAmount();
        }
    }
    public override void RefreshCasterBasedAmount(Unit caster)
    {
        foreach (Effect effect in projectile.destroyEffectList)
        {
            effect.RefreshCasterBasedAmount(caster);
        }
        foreach (Effect effect in projectile.impactEffectList)
        {
            effect.RefreshCasterBasedAmount(caster);
        }
    }
    public override void RefreshTargetBasedAmount(Unit target)
    {
        foreach (Effect effect in projectile.destroyEffectList)
        {
            effect.RefreshTargetBasedAmount(target);
        }
        foreach (Effect effect in projectile.impactEffectList)
        {
            effect.RefreshTargetBasedAmount(target);
        }
    }



















    public override void ActivateEffect()
    {
        StartCoroutine(Launch());
    }
    public override void ActivateEffect(Unit caster)
    {
        this.caster = caster;
        StartCoroutine(Launch());
    }
    public override void ActivateEffect(Unit caster, Unit target)
    {
        this.caster = caster;
        StartCoroutine(Launch());
    }
    public override void ActivateEffect(Unit caster, Unit target, float multiplier)
    {
        this.caster = caster;
        StartCoroutine(Launch());
    }
    public override void ActivateEffect(Unit caster, Unit target, ref float amount, float multiplier)
    {
        this.caster = caster;
        StartCoroutine(Launch());

        //instanceProjectile.Initialize(caster, target, multiplier);
        //instanceProjectile.FlyingStart();
    }

    IEnumerator Launch()
    {
        int currentCount = 0;
        float currentPeriod = float.MaxValue;
        while(true)
        {
            if(currentCount<launchCount)
            {
                if(currentPeriod<launchPeriod)
                {
                    currentPeriod += Time.deltaTime;
                }
                else
                {
                    LaunchMissile();
                    currentPeriod = 0;
                    currentCount++;
                }
            }
            yield return null;
        }
    }
    
    void LaunchMissile()
    {
        Projectile instanceProjectile = Instantiate(projectile);
        instanceProjectile.transform.position = launchPosition.transform.position + offset;
        instanceProjectile.GetComponent<Rigidbody2D>().velocity = launchVelocity;
        instanceProjectile.Initialize(caster);
        instanceProjectile.FlyStart();
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
