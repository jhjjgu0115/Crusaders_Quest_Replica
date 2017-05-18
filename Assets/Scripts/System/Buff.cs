using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Buff : MonoBehaviour
{
    string id;
    Unit caster;
    Unit applyTarget;

    public string ID
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
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
    public Unit ApplyTarget
    {
        get
        {
            return applyTarget;
        }
        set
        {
            applyTarget = value;
        }
    }

}
public partial class Buff : MonoBehaviour
{



    public void Activate()
    {

    }
}
