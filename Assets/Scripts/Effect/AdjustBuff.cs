using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AdjustBuff : Effect {

    public List<Buff> adjustBuffList;
    public E_ApplyTargetFilter applyTargetFilter;

    public override void ActivateEffect(Unit caster, List<Unit> target, float multiplier)
    {
        for (int index = 0; index < adjustBuffList.Count; index++)
        {
            switch (applyTargetFilter)
            {
                case E_ApplyTargetFilter.Caster:
                    caster.BuffManager.CreateOrOverLap(adjustBuffList[index], caster, target);
                    break;
                case E_ApplyTargetFilter.ApplyTarget:
                    if (target != null)
                    {
                        target.BuffManager.CreateOrOverLap(adjustBuffList[index], caster, target);
                    }
                    break;
                default:
                    if (target != null)
                    {
                        target.BuffManager.CreateOrOverLap(adjustBuffList[index], caster, target);
                    }
                    break;
            }
        }
    }
    public override void RefreshAllAmount(Unit caster, List<Unit> target)
    {

    }

}
