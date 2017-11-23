using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public partial class TestUnit : MonoBehaviour
{
    public string id = string.Empty;
    public string heroName = string.Empty;
    public E_HeroClass heroClass = E_HeroClass.None;
    public int level = 0;
    public E_GroupTag groupTag;

    StatManager statManager = new StatManager();
    BuffManager buffManager = new BuffManager();
    public StatManager StatManager
    {
        get
        {
            return statManager;
        }
    }
    public BuffManager BuffManager
    {
        get
        {
            return buffManager;
        }
    }
    //public Weapon weapon;
    //public Ring ring;

    //스킬 리스트 아니면 스킬사전
}
//상태
public partial class TestUnit : MonoBehaviour
{
    public bool isInBattle = false;
    public bool isAlive = false;
    public bool isGroggy = false;
    public bool isEntangle = false;
    public bool canInteraction = false;

    //전투진입
    //사망
    //부활
    //기절시작
    //기절종료
    //이동불가시작
    //이동불가종료
}
public partial class TestUnit : MonoBehaviour
{
    //행동 대기열
    //사정거리탐지
    //
}
public partial class TestUnit : MonoBehaviour
{
    /*
     * 피해를 받음
     * 피해 처리
     * 피해 이벤트
     * 
     * 회복을 받음
     * 회복 처리
     * 회복 이벤트
     * 
     * 스킬을 시전함
     * 스킬 이벤트
     * 스킬 처리
     * 
     * 회피함
     * 회피처리
     * 회피이벤트
     * 
     * 공격이 적중함
     * 적중 처리
     * 적중 이벤트
     * 
     */
}
public partial class TestUnit : MonoBehaviour
{
}
public partial class TestUnit : MonoBehaviour
{
}
public partial class TestUnit : MonoBehaviour
{
}
public partial class TestUnit : MonoBehaviour
{
}
public partial class TestUnit : MonoBehaviour
{
    public void DebugLog()
    {
        Debug.Log("--------" + name + "--------");
        foreach (E_StatType stat in Enum.GetValues(typeof(E_StatType)))
        {
            Debug.Log(statManager.Get_Stat(stat).StatName + " : " + statManager.Get_Stat(stat).BaseValue);
        }
    }
}
