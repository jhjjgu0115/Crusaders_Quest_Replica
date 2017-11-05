using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelectionSlot : MonoBehaviour
{
    public string heroName;
    public HeroInfo heroInfo;

    public Image outLineImage;
    public Sprite noneSelectedImage;
    public Sprite selectedImage;

    public Image heroPortrait;

    public HeroButton heroButton = null;


    public void SetLeaderThis()
    {
        int index = GameStartManager.Instance.selectionSlotList.IndexOf(this);
        if (GameStartManager.Instance.isSelectLeaderMode)
        {

        }
    }
    public void ClickEvent()
    {
        GameStartManager instance = GameStartManager.Instance;
        int indexOfThisSlot = GameStartManager.Instance.selectionSlotList.IndexOf(this);
        if(GameStartManager.Instance.isSelectLeaderMode)
        {
            if (heroInfo.name != string.Empty)
            {
                instance.SetLeader(indexOfThisSlot);
                instance.SelectLeaderModeEnd();
            }
        }
        else
        {
            if(heroInfo.name!= string.Empty)
            {
                instance.RemoveHero(instance.selectionInfoList[indexOfThisSlot].heroInfo);
            }
            //이 슬롯의 용사를 제거
        }
    }

    public void SetLeader(bool isLeader)
    {
        if(isLeader)
        {
            outLineImage.sprite = selectedImage;
        }
        else
        {
            outLineImage.sprite = noneSelectedImage;
        }
    }
    public void SetHero(HeroInfo heroInfo)
    {
        this.heroInfo = heroInfo;
        //리소스에서 해당 아이디의 용사 얼굴을 찾아옴.
        heroPortrait.color = new Color32(255, 255, 255, 255);
        for(int index=0;index<GameStartManager.Instance.HeroSpriteList.Length;index++)
        {
            if(GameStartManager.Instance.HeroSpriteList[index].name==(heroInfo.name+ "F"))
            {
                heroPortrait.sprite = GameStartManager.Instance.HeroSpriteList[index];
                break;
            }
        }
    }
    public void Clear()
    {
        heroPortrait.sprite = null;
        heroInfo.name = string.Empty;
        heroPortrait.color = new Color32(0, 0, 0, 0);
        SetLeader(false);
    }
    public void RemoveHero()
    {
        heroPortrait.sprite=null;
        heroInfo.name = string.Empty;
        heroPortrait.color = new Color32(0, 0, 0, 0);
        SetLeader(false);
        if(heroButton)
        {
            heroButton.Canceled();
            heroButton = null;
        }
    }

    void Start()
    {
        heroInfo.name = string.Empty;
    }
    private void Update()
    {
            heroName = heroInfo.name;
    }
}
