using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CrusadersQuestReplica
{
    public class ReferenceFloatValue : FloatValue
    {
        public Statisticalbe targetObject;
        public E_StatType statType;
        public float magnification;

        public override float GetValue()
        {
            return targetObject.Get_Stat(statType).ModifiedValue * magnification;
        }
    }
}
