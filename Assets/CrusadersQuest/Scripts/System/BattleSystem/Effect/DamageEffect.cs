using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CrusadersQuestReplica
{
    [Serializable]
    public class DamageEffect : Effect
    {
        public List<DamageSet> damageList = new List<DamageSet>();
        

        public override void Adjust(Unit caster, List<Unit> targetList)
        {
            targetList = this.targetList;
            foreach (Unit target in targetList)
            {

                
                    /*
                    DamageInfo damageInfo = new DamageInfo();
                    damageInfo.damageType = damage.damageType;
                    damageInfo.caster = caster;
                    damageInfo.damage = damage.casterBasedDamage.Result(caster) + damage.targetBasedDamage.Result(target) + damage.fixedDamage;
                    damageInfo.penetration = caster.statManager.CreateOrGetStat((E_StatType)damage.damageType).ModifiedValue +damage.casterBasedPenetration.Result(caster) + damage.targetBasedPenetration.Result(target) + damage.fixedPenetration;
                    target.GetDamage(caster, damageInfo);*/
                
            }
        }
    }

    [Serializable]
    public class DamageSet : ScriptableObject
    {
        public E_DamageType damageType;
        public Value casterBasedDamage;
        public Value targetBasedDamage;
        public Value fixedDamage;
        public Value casterBasedPenetration;
        public Value targetBasedPenetration;
        public Value fixedPenetration;
    }

}
