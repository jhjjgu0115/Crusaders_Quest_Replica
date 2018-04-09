using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrusadersQuestReplica
{
    [RequireComponent(typeof(Statisticalbe))]
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(Healable))]
    [RequireComponent(typeof(Buffable))]
    public class Unit : MonoBehaviour
    {
        Statisticalbe statComp;
        Buffable buffComp;
        Damageable damageComp;
        Healable healComp;

        string u_Id;
        string unitName;
    }
}

