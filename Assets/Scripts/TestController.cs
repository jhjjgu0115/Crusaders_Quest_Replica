using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour {

    public float x = 1;
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * x);
		
	}
}
