using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchMissileEffect : Effect
{
    public float launchAngle = 0;
    public float launchVelocity = 0;
    public GameObject launchPosition;
    public Vector3 offset;


    public Projectile projectile;
    public int launchCount=0;
    public float launchPeriod = 0;


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


        //instanceProjectile.Initialize(caster, target, multiplier);
        //instanceProjectile.FlyingStart();
    }

    IEnumerator Launch()
    {
        int currentCount = 0;
        float currentPeriod = 0;
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
        instanceProjectile.Initialize(caster, target);
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
