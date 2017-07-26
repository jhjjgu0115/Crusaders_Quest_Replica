using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager instance = null;
    public static BlockManager Instance
    {
        get
        {
            if (instance)
            {
                return instance;
            }
            else
            {
                instance = FindObjectOfType<BlockManager>();
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "GameManager";
                    instance = container.AddComponent<BlockManager>();
                }
                return instance;
            }
        }
    }
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    List<Block> blockList = new List<Block>();

    List<Unit> playerUnitList = null;



	// Use this for initialization
	void Start ()
    {
        playerUnitList = GameManager.PlayerUnitList;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BlockSkillActivate(int index,int chain)
    {
        //해당 체인의 스킬을 사용한다.
    }


    //블록 생성
    public void CreateBlock(Unit target,int number)
    {
        //해당 유닛의 블록을 생성.
        //BlockSet(target,스킬Index,스킬타입.노말or특수)
    }
    public void BlockSet()
    {
        //스킬타입,색상,해당유닛,시전스킬인덱스

    }
    //블록 합치기
    //해당 캐릭터에게 스킬 전달.
    //블록 소멸
    //블록 사용 불가
    //블록 변경

}
