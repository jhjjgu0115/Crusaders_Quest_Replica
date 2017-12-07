using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TestDestroy : MonoBehaviour
{
    public TestScript objec;
    public float count = 5;

	// Use this for initialization
	void Start () {
        objec.EventDel1 += MSG;
        
    }
	void MSG()
    {
        Debug.Log(this.name + "linked");
    }
    // Update is called once per frame
	void Update ()
    {
        if(count<0)
        {
            Destroy(gameObject);
        }

        count -= Time.deltaTime;
	}
    private void OnDestroy()
    {
        objec.EventDel1 -= MSG;
    }
}
