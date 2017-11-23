using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
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
    List<TestUnit> playableCharacterList = new List<TestUnit>();

    List<TestUnit> playableAliveCharacterList = new List<TestUnit>();
    public List<TestUnit> PlayableAliveCharacterList
    {
        get
        {
            return playableAliveCharacterList;
        }
    }

    TestUnit foremostPlayableCharacter;
    public TestUnit ForemostPlayableCharacter
    {
        get
        {
            for(int index =0; index<playableAliveCharacterList.Count; index++)
            {
                if(playableAliveCharacterList[index].transform.position.x>foremostPlayableCharacter.transform.position.x)
                {
                    foremostPlayableCharacter = playableAliveCharacterList[index];
                }
            }
            return foremostPlayableCharacter;
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
    public void FindLeaderEvent()
    {
        for(int index=0;index<playableAliveCharacterList.Count;index++)
        {
            //리더인지 확인후 해당 대상에게 이벤트 등록.
            //그리고 사망자는 이벤트 삭제.
            //playableAliveCharacterList[index];
        }
    }
}
//스테이지
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
                        playableAliveCharacterList.Add(testUnit);

                        foreach (E_StatType stat in Enum.GetValues(typeof(E_StatType)))
                        {
                            testUnit.StatManager.CreateOrGetStat(stat).BaseValue = float.Parse(node[stat.ToString()].InnerText);
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

}
public partial class StageManager : MonoBehaviour
{

}
public partial class StageManager : MonoBehaviour
{

    // Use this for initialization
    void Start ()
    {
        /*
         * 던전 생성.
         * 던전 이미지 배치[던전명을 기준으로]
         * 
         * 플레이어 캐릭터 배치
         * 플레이어 캐릭터 데이터 입력
         * 리더 설정
         * 블록 시스템 매니저 초기화
         * 블록 시스템 매니저에 플레이어 캐릭터 연결
         * 
         * 웨이브 리스트 초기화
         * 웨이브 리스트를 기준으로 몬스터 데이터 입력
         * 웨이브 리스트에 몬스터 삽입
         * 
         * 스테이지 이벤트 시스템 시작
         * 스테이지 시작
         * 
         */
        if (!instance)
        {
            instance = this;
        }
        selectedHeroList.Add(new SelectionInfo());
        selectedHeroList[0].heroInfo.name = "뮤";

        //StartLimitSync();
        LoadCharacterData();
        foreach(TestUnit unit in playableAliveCharacterList)
        {
            //unit.DebugLog();
        }
        //디버깅


    }
}
