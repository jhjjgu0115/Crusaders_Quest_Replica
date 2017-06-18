using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class AreaSearchEffect : Effect
{
    public List<Effect> enterEffectList = new List<Effect>();
    public List<Effect> periodEffectList = new List<Effect>();
    public List<Effect> exitEffectList = new List<Effect>();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (Effect effect in enterEffectList)
        {
            effect.ActivateEffect(caster, collision.GetComponent<Unit>());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach(Effect effect in periodEffectList)
        {
            effect.ActivateEffect(caster, collision.GetComponent<Unit>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (Effect effect in exitEffectList)
        {
            effect.ActivateEffect(caster, collision.GetComponent<Unit>());
        }
    }

}
