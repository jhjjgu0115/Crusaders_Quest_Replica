using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int penetrationCount = 0;//0이하는 무제한 관통
    int currentCount = 0;
    public float limitRange = 0;

    Vector3 launchPosition;


    Unit caster;
    public List<Effect> explosionEffectList = new List<Effect>();
    public List<Effect> impactEffectList = new List<Effect>();
    public List<Effect> destroyEffectList = new List<Effect>();
    public Model explosionModel;
    public Model impactModel;
    public Model destroyModel;

    private void Start()
    {
        launchPosition = transform.position;
    }
    
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
    public void Explosion()
    {
        if(explosionModel)
        {
            Instantiate(explosionModel).transform.position = transform.position;
        }
        foreach (Effect effect in explosionEffectList)
        {
            effect.ActivateEffect(caster);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Unit target = other.GetComponent<Unit>();
        if (currentCount < penetrationCount-1)
        {
            if(impactModel)
            {
                Instantiate(impactModel).transform.position = transform.position;
            }
            
            foreach (Effect effect in impactEffectList)
            {
                effect.RefreshTargetBasedAmount(target);
                effect.ActivateEffect(caster, target);
            }
            currentCount++;
        }
        else
        {
            if(currentCount< penetrationCount )
            {
                if (destroyModel)
                {
                    Instantiate(destroyModel).transform.position = transform.position;
                }
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                foreach (Effect effect in destroyEffectList)
                {
                    effect.RefreshTargetBasedAmount(target);
                    effect.ActivateEffect(caster, target);
                }
                currentCount++;
                StartCoroutine(DelayDestory());
            }
            
        }
    }
    IEnumerator DelayDestory()
    {
        yield return null;
        yield return null;
        Destroy(gameObject);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }








}
