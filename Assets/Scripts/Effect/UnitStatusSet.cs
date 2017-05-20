using UnityEngine;
using System.Collections;

public class UnitStatusSet : Effect
{
    bool SetStatus = false;
    public override void ActivateEffect(Unit caster, Unit target, float multiplier)
    {
        if (SetStatus == false)
        {
            SetStatus = true;
            target.FunctionStop();
        }
        else
        {
            SetStatus = false;
            target.FunctionStart();
        }
    }

}
