using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public partial class Effect : MonoBehaviour
{
    protected Unit caster;
    protected List<Unit> target;

    //protected List<Validator> validatorList = new List<Validator>();
}
public partial class Effect : MonoBehaviour
{
    public Effect()
    {
        this.caster = null;
        this.target = null;
    }
    public Effect(Unit caster, List<Unit> target)
    {
        this.caster = caster;
        this.target = target;
    }

    public Unit Caster
    {
        get
        {
            return caster;
        }
        set
        {
            caster = value;
        }
    }
    public List<Unit> Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }

    public virtual void ActivateEffect()
    {
    }
    public virtual void ActivateEffect(Unit caster, List<Unit> target)
    {
    }
    public virtual void ActivateEffect(Unit caster, List<Unit> target, float multiplier)
    {
    }
    public virtual void ActivateEffect(Unit caster, List<Unit> target, ref float amount, float multiplier)
    {
    }

    public virtual void RefreshAllAmount(Unit caster, List<Unit> target)
    {
    }
    public virtual void RefreshTargetBasedAmount(List<Unit> target)
    {
    }
    public virtual void RefreshCasterBasedAmount(Unit caster)
    {
    }
    public virtual void RefreshFixedAllAmount()
    {

    }


    protected virtual bool ConditionCheck()
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
