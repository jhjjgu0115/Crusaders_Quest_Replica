using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Block block;
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
     * 블록 풀 생성
     * 블록 풀 초기화
     * 블록 풀에서 꺼내기
     * 블록 풀에 반납(반납할 블록)
     */
    List<Block> blockPool;
    void InitializeBlockPool()
    {
        blockPool = new List<Block>();
        block = Resources.Load<Block>("Prefabs/System/BlockPanel/Block");
        lastBlockIndex = 0;
        for (int index = 0; index < 8; index++)
        {
            blockPool.Add(Instantiate<Block>(block));
            blockPool[index].transform.SetParent(transform, false);
            blockPool[index].SetBlockData(0, null,E_SkillType.Normal);
            blockPool[index].gameObject.SetActive(false);
        }
    }
    Block Pop()
    {
        foreach (Block block in blockPool)
        {
            if (!block.gameObject.activeInHierarchy)
            {
                block.gameObject.SetActive(true);
                block.SetBlockData(0, null, E_SkillType.Normal);
                return block;
            }
        }
        return null;
    }
    void Push(Block block)
    {
        block.gameObject.SetActive(false);
    }

    /* 
     * 블록 떨구기(떨굴 위치,떨구기
     * 블록 합치기
     * 블록 자동 생성 시작
     * 블록 자동 생성 중지
     */
     void DropDown()
    {

    }
    void Combine(params Block[] blocks)
    {

    }
    void StartGenerateBlock()
    {
        StartCoroutine(GenerateBlock());
    }
    void StopGenerateBlock()
    {
        StopCoroutine(GenerateBlock());
    }
    IEnumerator GenerateBlock()
    {
        while(true)
        {
            yield return null;
        }
    }










    Block GetBlockInPool()
    {
        foreach(Block block in blockPool)
        {
            if(!block.gameObject.activeInHierarchy)
            {
                block.gameObject.SetActive(true);
                return block;
            }
        }
        return null;
    }
    public void ReturnBlockPool(Block block)
    {
        block.gameObject.SetActive(false);
    }

    //블록 주기 생성
    List<Unit> playerUnitList = null;
    public float blockGeneratePeriod = 0;
    float currentGeneratingPeriod = 0;
    
    /// <summary>
    /// 자연 블록 생성
    /// </summary>
    /// <returns></returns>
    IEnumerator GeneratingBlockPeriodic()
    {

        while (true)
        {
            if(lastBlockIndex<8)
            {
                if (currentGeneratingPeriod < blockGeneratePeriod)
                {
                    currentGeneratingPeriod += Time.deltaTime;
                }
                else
                {
                    currentGeneratingPeriod = 0;
                    Block tempBlock = GetBlockInPool();
                    //Debug.Log(GetComponent<RectTransform>().rect.xMax);
                    //tempBlock.transform.position = 
                    tempBlock.SetBlockData(3, GameManager.playerHeadUnit,E_SkillType.Normal);
                    StartCoroutine(DropDown(tempBlock,blockPanelPostionList[lastBlockIndex]));
                    lastBlockIndex++;
                    //블록 생성.
                }
            }
            else
            {
                currentGeneratingPeriod = 0;
            }


            yield return null;
        }
    }

    //블록 패널
    public int lastBlockIndex = 0;
    public List<Transform> blockPanelPostionList = new List<Transform>();
    public List<Block> blockPanel = new List<Block>();

    IEnumerator DropDown(Block dropBlock,Transform targetPostion)
    {
        while (Vector3.Distance(targetPostion.position, dropBlock.transform.position) >= 0.4f)
        {
            dropBlock.transform.position = Vector3.Lerp(targetPostion.position, dropBlock.transform.position, 0.4f);
            yield return null;
        }
        dropBlock.transform.position = targetPostion.position;
        dropBlock.canUse = true;
        yield return null;
    }

  

    void CreateBlock()
    {

    }
    

    // Use this for initialization
    void Start ()
    {
        InitializeBlockPool();
        playerUnitList = GameManager.Instance.PlayerUnitList;
        StartCoroutine(GeneratingBlockPeriodic());
        for(int index=0;index<8;index++)
        {
            blockPanel.Add(null);
        }

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
