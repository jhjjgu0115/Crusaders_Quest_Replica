using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CrusadersQuestReplica
{
    [System.Serializable]
    public class Effect : MonoBehaviour
    {
        public Unit caster;
        public List<Unit> targetList=new List<Unit>();

        public virtual void Adjust()
        {

        }
        public virtual void Adjust(Unit caster)
        {

        }
        public virtual void Adjust(Unit caster, List<Unit> targetList)
        {

        }
    }
}
