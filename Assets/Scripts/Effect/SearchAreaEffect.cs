using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SearchAreaEffect : Effect
{
    //원일때 탐색 각도
    //각도
    //최대 포착
    //최소 포착
    
    //서칭 옵션
    //뒤에서부터, 앞에서부터,랜덤
    //



    public int maximumCount = 0;
    public List<Effect> enterEffectList = new List<Effect>();
    int currentCount = 0;


    private void OnTriggerEnter2D(Collider2D targetCollider)
    {
        if(currentCount<maximumCount)
        {
            Debug.Log(currentCount);
            currentCount++;
            foreach (Effect effect in enterEffectList)
            {
                effect.ActivateEffect(caster, targetCollider.GetComponent<Unit>());
            }
        }


    }
    private void OnEnable()
    {
        currentCount = 0;
    }
}
