using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    List<Unit> playableCharacterList = new List<Unit>();

    List<Unit> playableAliveCharacterList = new List<Unit>();
    public List<Unit> PlayableAliveCharacterList
    {
        get
        {
            return playableAliveCharacterList;
        }
    }

    Unit foremostPlayableCharacter;
    public Unit ForemostPlayableCharacter
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


    Unit leader;
    public Unit Leader
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

    void LoadCharacterData()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("Assets/Resources/XML/Status_Data.xml");
        foreach (XmlNode node in xmlDoc.DocumentElement)
        {
            Debug.Log(node["class"].InnerText);
        }
    }

}
//스테이지
public partial class StageManager : MonoBehaviour
{
    //스테이지 이동 제한
    public GameObject stageLimit;
    void StartLimitSync()
    {
        StartCoroutine(LimitSync());
    }
    IEnumerator LimitSync()
    {
        while (true)
        {
            if (stageLimit.transform.position.x < foremostPlayableCharacter.transform.position.x)
            {
                stageLimit.transform.position = new Vector3(foremostPlayableCharacter.transform.position.x, 0, 0);
            }
            yield return null;
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

        //StartLimitSync();
        LoadCharacterData();

    }
}
