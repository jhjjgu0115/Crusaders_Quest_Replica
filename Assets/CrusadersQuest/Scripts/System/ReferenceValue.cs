using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrusadersQuestReplica
{
    [System.Serializable]
    public class ReferenceValue
    {
        public E_StatType statType;
        public float magnification;
        public float Result(Unit target)
        {
            return target.statManager.CreateOrGetStat(statType).ModifiedValue*magnification;
        }
    }

}