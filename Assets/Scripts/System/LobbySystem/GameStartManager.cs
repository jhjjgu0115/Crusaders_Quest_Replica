using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectionInfo
{
    public HeroInfo heroInfo;
    public bool isLeader;
    public HeroButton heroButton;
}
public partial class GameStartManager : MonoBehaviour
{
    static GameStartManager instance;
    public static GameStartManager Instance
    {
        get
        {
            return instance;
        }
    }
}
public partial class GameStartManager : MonoBehaviour
{
    Sprite[] sprites;
    Dictionary<string, Sprite> portraitDict = new Dictionary<string, Sprite>();
    Dictionary<E_HeroClass, Sprite> classIconDict = new Dictionary<E_HeroClass, Sprite>();
    public Dictionary<E_HeroClass, Sprite> ClassIconDict
    {
        get
        {
            return classIconDict;
        }
    }
    public Dictionary<string, Sprite> PortraitDict
    {
        get
        {
            return portraitDict;
        }
    }
    List<Sprite> CharacterSpriteList = new List<Sprite>();
    public List<Sprite> classIconList = new List<Sprite>();

}
public partial class GameStartManager : MonoBehaviour
{
}
public partial class GameStartManager : MonoBehaviour
{
    public HeroButton heroButtonPrefab;
    public GridLayoutGroup gridLayout;
    HeroButton AddCharacterButton(string id, E_HeroClass heroClass)
    {
        HeroButton temp = Instantiate(heroButtonPrefab);
        temp.name = count.ToString();
        count++;
        temp.transform.SetParent(gridLayout.transform);
        temp.heroInfo.name = id;
        temp.heroInfo.heroClass = heroClass;
        for (int index = 0; index < CharacterSpriteList.Count; index++)
        {
            if (CharacterSpriteList[index].name == (id))
            {
                temp.heroImage.sprite = CharacterSpriteList[index];
                temp.classIcon.sprite = classIconDict[heroClass];
                break;
            }
        }
        return temp;
    }
    void SetGridLayoutFit()
    {
        int countInGrid = gridLayout.transform.childCount;
        RectTransform rectTransform = gridLayout.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, ((countInGrid / 9)* 43+4));
    }
}
public partial class GameStartManager : MonoBehaviour
{
    int count = 0;
    public List<SelectionInfo> selectionInfoList = new List<SelectionInfo>(3);
    public List<SelectionSlot> selectionSlotList = new List<SelectionSlot>();

    public bool isSelectLeaderMode = false;

    public GameObject leaderSelectionModeHighlight;


    public void SetLeader(int leaderIndex)
    {
        for(int index=0;index< selectionInfoList.Count; index++)
        {
            selectionInfoList[index].isLeader = false;
            selectionSlotList[index].SetLeader(false);
        }
        selectionInfoList[leaderIndex].isLeader = true;
        selectionSlotList[leaderIndex].SetLeader(true);
    }
    public void AddHero(HeroInfo heroInfo, HeroButton heroButton)
    {
        SelectionInfo addHeroInfo = new SelectionInfo();
        addHeroInfo.heroInfo = heroInfo;
        addHeroInfo.isLeader = false;
        addHeroInfo.heroButton = heroButton;

        if (selectionInfoList.Count==0)
        {
            selectionInfoList.Add(addHeroInfo);
            selectionInfoList[0].isLeader = true;
        }
        else
        {//클래스 비교후 삽입
            int insertIndex = 0;
            for (int index = 0; index < selectionInfoList.Count; index++)
            {
                if (addHeroInfo.heroInfo.heroClass> selectionInfoList[index].heroInfo.heroClass)
                {
                    insertIndex = index;
                    break;
                }
                else
                {
                    insertIndex++;
                }
            }
            selectionInfoList.Insert(insertIndex,addHeroInfo);
        }
        SyncAllList();
    }
    public void RemoveHero(HeroInfo heroInfo,HeroButton heroButton)
    {
        SelectionInfo removeHeroInfo = selectionInfoList.Find(info => info.heroButton == heroButton);
        selectionInfoList.Remove(removeHeroInfo);
        removeHeroInfo.heroButton.Canceled();
        if(selectionInfoList.Count>0)
        {
            if(removeHeroInfo.isLeader)
            {
                selectionInfoList[0].isLeader = true;
            }
        }


        SyncAllList();
    }
    void SyncAllList()
    {
        for(int index=0;index < 3;index++)
        {
            if(index<selectionInfoList.Count)
            {
                selectionSlotList[index].SetHero(selectionInfoList[index].heroInfo);
                selectionSlotList[index].heroButton = selectionInfoList[index].heroButton;
                selectionSlotList[index].SetLeader(selectionInfoList[index].isLeader);
            }
            else
            {
                selectionSlotList[index].Clear();
            }
        }

    }
   
    public void ToggleSelectLeaderMode()
    {
        if(isSelectLeaderMode)
        {
            isSelectLeaderMode = false;
            leaderSelectionModeHighlight.SetActive(false);
        }
        else
        {
            isSelectLeaderMode = true;
            leaderSelectionModeHighlight.SetActive(true);
        }


    }
    public void SelectLeaderModeEnd()
    {
        isSelectLeaderMode = false;
        leaderSelectionModeHighlight.SetActive(false);
    }


}
public partial class GameStartManager : MonoBehaviour
{
    void Start()
    {
        if (Instance == null)
        {
            instance = this;
        }
        sprites = Resources.LoadAll<Sprite>("LobbySystem/Sprites/AllHero");
        List<Sprite> tempSpriteList = new List<Sprite>(sprites);
        foreach(Sprite sprite in tempSpriteList.FindAll(temp => temp.name[temp.name.Length - 1] == 'F'))
        {
            portraitDict.Add(sprite.name, sprite);
        }
        CharacterSpriteList = new List<Sprite>(tempSpriteList.FindAll(temp => temp.name[temp.name.Length - 1] != 'F'));

        tempSpriteList = new List<Sprite>(Resources.LoadAll<Sprite>("LobbySystem/Sprites/WindowUI"));
        tempSpriteList = tempSpriteList.FindAll(s => s.name.Substring(0, 4) == "Icon");
        classIconDict.Add(E_HeroClass.Wizard, tempSpriteList.Find(s => s.name.Substring(4) == E_HeroClass.Wizard.ToString()));
        classIconDict.Add(E_HeroClass.Hunter, tempSpriteList.Find(s => s.name.Substring(4) == E_HeroClass.Hunter.ToString()));
        classIconDict.Add(E_HeroClass.Archer, tempSpriteList.Find(s => s.name.Substring(4) == E_HeroClass.Archer.ToString()));
        classIconDict.Add(E_HeroClass.Priest, tempSpriteList.Find(s => s.name.Substring(4) == E_HeroClass.Priest.ToString()));
        classIconDict.Add(E_HeroClass.Paladin, tempSpriteList.Find(s => s.name.Substring(4) == E_HeroClass.Paladin.ToString()));
        classIconDict.Add(E_HeroClass.Worrior, tempSpriteList.Find(s => s.name.Substring(4) == E_HeroClass.Worrior.ToString()));


        /*이구간이 XML에서 데이터 읽어와 캐릭터를 그리드에 뿌린다.*/
        AddCharacterButton("레온", E_HeroClass.Worrior);
        AddCharacterButton("이사벨", E_HeroClass.Worrior);
        AddCharacterButton("빅토리아", E_HeroClass.Worrior);
        AddCharacterButton("잔다르크", E_HeroClass.Worrior);
        AddCharacterButton("크림힐트", E_HeroClass.Paladin);
        AddCharacterButton("라히마", E_HeroClass.Archer);
        AddCharacterButton("아칸", E_HeroClass.Wizard);
        AddCharacterButton("바이퍼", E_HeroClass.Hunter);
        AddCharacterButton("멜리사", E_HeroClass.Priest);
        AddCharacterButton("레온", E_HeroClass.Worrior);
        AddCharacterButton("이사벨", E_HeroClass.Worrior);
        AddCharacterButton("빅토리아", E_HeroClass.Worrior);
        AddCharacterButton("잔다르크", E_HeroClass.Worrior);
        AddCharacterButton("크림힐트", E_HeroClass.Paladin);
        AddCharacterButton("라히마", E_HeroClass.Archer);
        AddCharacterButton("아칸", E_HeroClass.Wizard);
        AddCharacterButton("바이퍼", E_HeroClass.Hunter);
        AddCharacterButton("멜리사", E_HeroClass.Priest);
        AddCharacterButton("레온", E_HeroClass.Worrior);
        AddCharacterButton("이사벨", E_HeroClass.Worrior);
        AddCharacterButton("빅토리아", E_HeroClass.Worrior);
        AddCharacterButton("잔다르크", E_HeroClass.Worrior);
        AddCharacterButton("크림힐트", E_HeroClass.Paladin);
        AddCharacterButton("라히마", E_HeroClass.Archer);
        AddCharacterButton("아칸", E_HeroClass.Wizard);
        AddCharacterButton("바이퍼", E_HeroClass.Hunter);
        AddCharacterButton("멜리사", E_HeroClass.Priest);
        AddCharacterButton("레온", E_HeroClass.Worrior);
        AddCharacterButton("이사벨", E_HeroClass.Worrior);
        AddCharacterButton("빅토리아", E_HeroClass.Worrior);
        AddCharacterButton("잔다르크", E_HeroClass.Worrior);
        AddCharacterButton("크림힐트", E_HeroClass.Paladin);
        AddCharacterButton("라히마", E_HeroClass.Archer);
        AddCharacterButton("아칸", E_HeroClass.Wizard);
        AddCharacterButton("바이퍼", E_HeroClass.Hunter);
        AddCharacterButton("멜리사", E_HeroClass.Priest);
        AddCharacterButton("레온", E_HeroClass.Worrior);
        AddCharacterButton("이사벨", E_HeroClass.Worrior);
        AddCharacterButton("빅토리아", E_HeroClass.Worrior);
        AddCharacterButton("잔다르크", E_HeroClass.Worrior);
        AddCharacterButton("크림힐트", E_HeroClass.Paladin);
        AddCharacterButton("라히마", E_HeroClass.Archer);
        AddCharacterButton("아칸", E_HeroClass.Wizard);
        AddCharacterButton("바이퍼", E_HeroClass.Hunter);
        AddCharacterButton("멜리사", E_HeroClass.Priest);
        AddCharacterButton("레온", E_HeroClass.Worrior);
        AddCharacterButton("이사벨", E_HeroClass.Worrior);
        AddCharacterButton("빅토리아", E_HeroClass.Worrior);
        AddCharacterButton("잔다르크", E_HeroClass.Worrior);
        AddCharacterButton("크림힐트", E_HeroClass.Paladin);
        AddCharacterButton("라히마", E_HeroClass.Archer);
        AddCharacterButton("아칸", E_HeroClass.Wizard);
        AddCharacterButton("바이퍼", E_HeroClass.Hunter);
        AddCharacterButton("멜리사", E_HeroClass.Priest);



        SetGridLayoutFit();
    }
}
    