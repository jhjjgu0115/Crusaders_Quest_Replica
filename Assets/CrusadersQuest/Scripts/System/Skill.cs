using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrusadersQuestReplica
{
    [System.Serializable]
    public class Skill:MonoBehaviour
    {
        public string skillName=string.Empty;
        public List<Effect> effectList = new List<Effect>();
        
        public void Activate(Unit caster)
        {
            foreach(Effect effect in effectList)
            {
                effect.Adjust(caster,new List<Unit>());
            }
        }
    }
}


