using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrusadersQuestReplica
{
    public class FixedFloatValue : FloatValue
    {
        public float value=0;

        public override float GetValue()
        {
            return value;
        }
    }
}

