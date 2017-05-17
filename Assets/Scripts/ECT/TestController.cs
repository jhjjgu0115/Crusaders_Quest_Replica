using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour {
    public Vector3 startPosion = new Vector3(0,-1.956f,0);
    public float x = 1;
    public Animator animator;
    bool isEnd = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update () {
        transform.position = Vector3.MoveTowards(transform.position, startPosion, Time.deltaTime * x);
        if(transform.position.x<=1.0f && !isEnd)
        {
            isEnd = true;
            animator.Play("Victory",0,0.01f);
            animator.SetBool("inBattle", false);
        }
		
	}
}
