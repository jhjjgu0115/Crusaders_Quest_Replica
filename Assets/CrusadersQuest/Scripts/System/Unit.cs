using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrusadersQuestReplica
{
    public class Unit : MonoBehaviour
    {
        public Animator animator;

        public StatManager statManager = new StatManager();
        public BuffManager buffManager = new BuffManager();
        public SkillManager skillManager = new SkillManager();

        public void Act()
        {
            animator.Play("Attack1");
        }
        public void CastSkill(int index)
        {
            skillManager.skillList[index].Activate(this);
        }

        public event DamageEvent DamageEvent;
        public void GetDamage(Unit attacker,DamageInfo damageInfo)
        {
            if(DamageEvent!=null)
            {
                DamageEvent(this, damageInfo);
            }
            AdjustDamage(damageInfo);
        }
        void AdjustDamage(DamageInfo damageInfo)
        {
            float defensivePower = statManager.CreateOrGetStat((E_StatType)damageInfo.damageType).ModifiedValue;
            float damageReduce = 0;
            if (damageInfo.penetration >= defensivePower)
            {
                damageReduce = 1;
            }
            else
            {
                damageReduce = (100 / (((defensivePower - damageInfo.penetration) * 0.348f) + 100));
            }
            damageInfo.damage *= damageReduce;


            E_FloatingType floatingType = E_FloatingType.NonpenetratingDamage;
            if (damageReduce > 0.85f)
            {
                floatingType = E_FloatingType.FullPenetrationDamage;
            }
            float currentHP = statManager.CreateOrGetStat(E_StatType.CurrentHealth).ModifiedValue;
            statManager.CreateOrGetStat(E_StatType.CurrentHealth).ModifiedValue -= damageInfo.damage;
            FloatingNumberManager.FloatingNumber(gameObject, damageInfo.damage, floatingType);
        }

    }
}
