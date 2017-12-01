using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.SceneManagement;
//전역 데이터
public partial class StageManager : MonoBehaviour
{ 
    static StageManager instance;
    public static StageManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<StageManager>();
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "StageManager";
                    instance = container.AddComponent<StageManager>();
                }
            }
            return instance;
        }
    }
    public static List<SelectionInfo> selectedHeroList = new List<SelectionInfo>(3);
    public static string StageName="Winter";
}
//캐릭터
public partial class StageManager : MonoBehaviour
{
    ExtendedList<TestUnit> totalHeroList = new ExtendedList<TestUnit>();
    public ExtendedList<TestUnit> TotalHeroList
    {
        get
        {
            return totalHeroList;
        }
    }
    ExtendedList<TestUnit> aliveHeroList = new ExtendedList<TestUnit>();
    public ExtendedList<TestUnit> AliveHeroList
    {
        get
        {
            return aliveHeroList;
        }
    }
    ExtendedList<TestUnit> deadHeroList = new ExtendedList<TestUnit>();
    public ExtendedList<TestUnit> DeadHeroList
    {
        get
        {
            return deadHeroList;
        }
    }
    
    TestUnit foremostHero;
    public TestUnit ForemostHero
    {
        get
        {
            for(int index =0; index< aliveHeroList.Count; index++)
            {
                if(aliveHeroList[index].transform.position.x>foremostHero.transform.position.x)
                {
                    foremostHero = aliveHeroList[index];
                }
            }
            return foremostHero;
        }
    }
    TestUnit leader;
    public TestUnit Leader
    {
        get
        {
            return leader;
        }
    }

    public void HeroDead(TestUnit deadUnit)
    {

    }
    public void HeroReBirth(TestUnit deadUnit)
    {

    }














    public void Victory(int count)
    {
        if (count == 0)
        {
            Debug.Log("Victory");
        }
    }
    public void Defeat(int count)
    {
        if(count ==0)
        {
            Debug.Log("Defeat");
        }
    }

}
//스테이지 제어
public partial class StageManager : MonoBehaviour
{
    public Transform[] partyPosition;

    //스테이지 이동 제한
    public GameObject stageLimit;
    //웨이브 로드 될때 캐릭터를 기준으로 이동시킨다.




}
//스테이지 외적 요소
public partial class StageManager : MonoBehaviour
{
    void LoadCharacterData()
    {
        if(selectedHeroList.Count==0)
        {
            Debug.Log("선택된 용사 리스트가 비어잇습니다.");
        }
        else
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Assets/Resources/XML/Ingame_Data.xml");
            foreach (SelectionInfo selectedHero in selectedHeroList)
            {
                foreach (XmlNode node in xmlDoc.DocumentElement)
                {
                    if (node["name"].InnerText == selectedHero.heroInfo.name)
                    {
                        TestUnit testUnit = Instantiate<TestUnit>(Resources.Load<TestUnit>("Hero/Test/" + node["name"].InnerText));
                        aliveHeroList.Add(testUnit);

                        foreach (E_StatType stat in Enum.GetValues(typeof(E_StatType)))
                        {
                            testUnit.StatManager.AddStat(stat, new StatFloat(stat, stat.ToString(), float.Parse(node[stat.ToString()].InnerText)));
                        }
                        testUnit.heroClass = (E_HeroClass)Enum.Parse(typeof(E_HeroClass), node["class"].InnerText);
                        testUnit.id = node["id"].InnerText;
                        testUnit.heroName = node["name"].InnerText;
                        testUnit.level = int.Parse(node["level"].InnerText);
                    }
                }
            }
        }
        
    }
}
public partial class StageManager : MonoBehaviour
{   
    
    //웨이브가 모두 종료되면 승리
    //생존 파티원 = 0 이면 패배
}
public partial class StageManager : MonoBehaviour
{

}
public partial class StageManager : MonoBehaviour
{

    // Use this for initialization
    void Start ()
    {
        if (!instance)
        {
            instance = this;
        }
        aliveHeroList.CountListener += Defeat;
        //웨이브가 만료되면 승리

        
        //StartLimitSync();
        LoadCharacterData();
        foreach(TestUnit unit in aliveHeroList)
        {
            //unit.DebugLog();
        }
        //디버깅


    }
}
