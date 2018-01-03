using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTest : MonoBehaviour
{
    public DEffect df;
}

[CreateAssetMenu(fileName ="NewDEffect",menuName ="Effect/Damage",order = 0)]
public class DEffect : ScriptableObject
{
    public int damage;
    public int b;
}