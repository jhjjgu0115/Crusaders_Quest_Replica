using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrusadersQuestReplica
{
    public class DamageEffect : Effect
    {
        public List<BasedOnDamage> damageList = new List<BasedOnDamage>();

        public override void Adjust(Unit caster, List<Unit> targetList)
        {
            targetList = this.targetList;
            foreach (Unit target in targetList)
            {
                foreach (BasedOnDamage damage in damageList)
                {
                    DamageInfo damageInfo = new DamageInfo();
                    damageInfo.damageType = damage.damageType;
                    damageInfo.caster = caster;
                    damageInfo.damage = damage.casterBasedDamage.Result(caster) + damage.targetBasedDamage.Result(target) + damage.fixedDamage;
                    damageInfo.penetration = caster.statManager.CreateOrGetStat((E_StatType)damage.damageType).ModifiedValue +damage.casterBasedPenetration.Result(caster) + damage.targetBasedPenetration.Result(target) + damage.fixedPenetration;
                    target.GetDamage(caster, damageInfo);
                }
            }
        }



    }
    [System.Serializable]
    public struct BasedOnDamage
    {
        public E_DamageType damageType;
        public ReferenceValue casterBasedDamage;
        public ReferenceValue targetBasedDamage;
        public float fixedDamage;
        public ReferenceValue casterBasedPenetration;
        public ReferenceValue targetBasedPenetration;
        public float fixedPenetration;
    }

}
