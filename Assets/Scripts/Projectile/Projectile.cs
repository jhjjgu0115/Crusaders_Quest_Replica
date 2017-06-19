using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isHoming = false;
    public int penetrationCount = 0;//0이하는 무제한 관통
    



    public List<Effect> enterEffectList = new List<Effect>();
    public List<Effect> stayrEffectList = new List<Effect>();
    public List<Effect> exitEffectList = new List<Effect>();

    public void Initialize(Unit caster, Unit target)
    {

    }
    public void FlyStart()
    {
        StartCoroutine(Flying());
    }
    IEnumerator Flying()
    {
        while(true)
        {
            yield return null;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        
    }

}
