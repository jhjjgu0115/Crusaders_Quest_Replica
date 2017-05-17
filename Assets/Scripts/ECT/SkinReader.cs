using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Anima2D;

namespace CrusadersQuest
{
    public class SkinReader : MonoBehaviour
    {
        [SerializeField]
        [HideInInspector]
        List<Pose> m_Poses;
    }
}
