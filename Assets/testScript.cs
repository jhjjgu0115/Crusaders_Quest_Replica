using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour {
    public testScript target;
    public List<test> effectList=new List<test>();
    public test effect;

	// Use this for initialization
	void Start ()
    {
        test tempEffect = Instantiate(effect);
        if (target)
        {
            Debug.Log(name);
            
            Debug.Log("made " + tempEffect.name);
            if (!target.effectList.Contains(tempEffect))
            {
                target.effectList.Add(tempEffect);
            }
            else
            {
                Debug.Log("exist");
            }
        }
        if (target)
        {
            Debug.Log(name);
            Debug.Log("made " + tempEffect.name);
            if (!target.effectList.Contains(tempEffect))
            {
                target.effectList.Add(tempEffect);
            }
            else
            {
                Debug.Log("exist");
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
