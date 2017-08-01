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


    /*
     *블록 풀을 만들어 제어 
     * 
     */

    public List<Block> blockPool = new List<Block>(8);

    void InitializeBlockPool()
    {
        for(int index=0; index<8; index++)
        {
            blockPool[index] = new Block();
            blockPool[index].Initialize(0, null);
        }
    }















    List<Block> blockList = new List<Block>();
    List<Unit> playerUnitList = null;

    Transform lastIndex;

    IEnumerator DropDown(Block dropBlock)
    {
        while (Vector3.Distance(dropBlock.transform.position, lastIndex.position) >= 0.4f)
        {
            dropBlock.transform.position = Vector3.Lerp(transform.position, lastIndex.position, 0.4f);
            yield return null;
        }
        dropBlock.transform.position = lastIndex.position;
        yield return null;
    }

    public float blockGeneratePeriod = 0;
    float currentGeneratingPeriod = 0;
    /// <summary>
    /// 자연 블록 생성
    /// </summary>
    /// <returns></returns>
    IEnumerator GeneratingBlockPeriodic()
    {

        while(true)
        {
            if(currentGeneratingPeriod > 0)
            {
                currentGeneratingPeriod -= Time.deltaTime;
            }
            else
            {
                //블록 생성.
            }
            yield return null;
        }
    }

    void CreateBlock()
    {

    }


    // Use this for initialization
    void Start ()
    {
        playerUnitList = GameManager.Instance.PlayerUnitList;

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
