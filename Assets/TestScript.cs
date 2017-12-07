using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    public TestDestroy gameobject;

    public delegate void testdelegate();
    public event testdelegate EventDel1;
    event testdelegate EventDel2;
    testdelegate Del;
	// Use this for initialization
	void Start ()
    {
        if (EventDel1 != null)
            EventDel1();
        if (EventDel2 != null)
            EventDel2();
        if (Del != null)
            Del.Invoke();
    }

    void Msg1()
    {
        Debug.Log(1);
    }
    void Msg2()
    {
        Debug.Log(2);
    }
    void Msg3()
    {
        Debug.Log(3);
    }

    // Update is called once per frame
    void Update ()
    {
        if(EventDel1!=null)
        {
            EventDel1();
        }
		
	}
}
