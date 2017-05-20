using UnityEngine;
using System.Collections;

public class RemoveBuff : Effect {

    /// <summary>
    /// -1이면 모두 제거. 일단은 기본적으로 버프 삭제 추후 추가바람
    /// </summary>
    public string buffId;
    public E_ApplyTargetFilter targetFilter;

    public override void ActivateEffect(Unit caster, Unit target, float multiplier)
    {
        if(ConditionCheck())
        {
            switch (targetFilter)
            {
                case E_ApplyTargetFilter.Caster:
                    if (caster.BuffManager.Contains(buffId))
                    {
                        caster.BuffManager.RemoveBuff(buffId);
                    }
                    break;
                case E_ApplyTargetFilter.ApplyTarget:
                    if (target.BuffManager.Contains(buffId))
                    {
                        target.BuffManager.RemoveBuff(buffId);
                    }
                    break;
                default:
                    if (target.BuffManager.Contains(buffId))
                    {
                        target.BuffManager.RemoveBuff(buffId);
                    }
                    break;
            }
        }

    }

    protected override bool ConditionCheck()
    {
        for (int index = 0; index < validatorList.Count; index++)
        {
            if (!validatorList[index].Check(caster, target))
            {
                return false;
            }
        }
        return true;
    }
}
