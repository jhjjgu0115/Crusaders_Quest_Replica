﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrusadersQuestReplica
{
    public class DamageInfo : Info
    {
        public Unit caster;
        public E_DamageType damageType;
        public float damage;
        public float penetration;
    }
}