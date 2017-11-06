﻿using System.Collections;
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
    public Image classIcon;
    public Image classIconBack;


    public HeroButton heroButton = null;


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
                instance.RemoveHero(instance.selectionInfoList[indexOfThisSlot].heroInfo, instance.selectionInfoList[indexOfThisSlot].heroButton);
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
        classIcon.color = new Color32(255, 255, 255, 255);
        classIconBack.color = new Color32(0, 0, 0, 150);
        heroPortrait.sprite = GameStartManager.Instance.PortraitDict[heroInfo.name+"F"];
        classIcon.sprite = GameStartManager.Instance.ClassIconDict[heroInfo.heroClass];
    }
    public void Clear()
    {
        heroInfo.name = string.Empty;
        SetLeader(false);
        heroPortrait.sprite = null;
        heroPortrait.color = new Color32(0, 0, 0, 0);
        classIcon.sprite = null;
        classIcon.color = new Color32(0, 0, 0, 0);
        classIconBack.color = new Color32(0, 0, 0, 0);

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
