﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int penetrationCount = 0;//0이하는 무제한 관통
    int currentCount = 0;
    public float limitRange = 0;

    Vector3 launchPosition;
    Vector3 targetPosition;


    Unit caster;
    Unit target;

    public List<Effect> impactEffectList = new List<Effect>();

    private void Start()
    {
        launchPosition = transform.position;
    }

    float directionMagnitude;
    
    public void Initialize(Unit _caster)
    {

        caster = _caster;
        launchPosition = transform.position;
    }



    public void FlyStart()
    {
        currentCount = 0;
        StartCoroutine(Flying());
    }
    IEnumerator Flying()
    {
        

        float currentDistance = 0;
        while(true)
        {
            if(currentDistance < limitRange)
            {
                currentDistance = (transform.position - launchPosition).magnitude;
            }
            else
            {
                Destroy(gameObject);
            }

            yield return null;
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentCount < penetrationCount-1)
        {
            foreach (Effect effect in impactEffectList)
            {
                effect.ActivateEffect(caster, other.GetComponent<Unit>());
            }
            currentCount++;
        }
        else
        {
            foreach (Effect effect in impactEffectList)
            {
                effect.ActivateEffect(caster, other.GetComponent<Unit>());
                GetComponent<Animator>().Play("Impact");
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        }
    }
    
    public void DestroyThis()
    {
        Destroy(gameObject);
    }










}
