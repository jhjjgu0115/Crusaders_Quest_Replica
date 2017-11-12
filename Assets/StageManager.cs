using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static List<SelectionInfo> selectedHeroList = new List<SelectionInfo>(3);

	// Use this for initialization
	void Start ()
    {
        foreach (SelectionInfo selectedHero in selectedHeroList)
        {
            if (selectedHero.isLeader)
            {
                Debug.Log("[Leader]" + selectedHero.heroInfo.name);
            }
            else
            {
                Debug.Log("[Sub]" + selectedHero.heroInfo.name);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
