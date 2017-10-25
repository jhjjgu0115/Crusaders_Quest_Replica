using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollableObject : MonoBehaviour
{
    public float minX=-7.7f;
    public float maxX=2.4f;
    public float dragSpeed=1;
    public float velocity;
    public float smooth;

    public static ScrollableObject coroutineObject;
    public static Coroutine scrollCoroutine;


    private void OnMouseDrag()
    {
        float x = Input.GetAxis("Mouse X");
        velocity = x;
        CameraController.instance.transform.position += new Vector3(x* dragSpeed, 0, 0);
    }
    private void OnMouseDown()
    {
        if (scrollCoroutine!=null)
        {
            coroutineObject.StopCoroutine(scrollCoroutine);
        }

    }

    private void OnMouseUp()
    {
        if (scrollCoroutine != null)
        {
            coroutineObject.StopCoroutine(scrollCoroutine);
        }
        coroutineObject = this;
        scrollCoroutine = StartCoroutine(SmoothMoving());
    }
    
    IEnumerator SmoothMoving()
    {
        while(true)
        {
            CameraController.instance.transform.position += new Vector3(velocity * dragSpeed, 0, 0);
            velocity *= smooth;
            if(velocity>0.01f || velocity < -0.01f)
            {
            }
            else
            {
                break;
            }
            yield return null;
        }
    }


    // Use this for initialization
    void Start ()
    {
	}


    // Update is called once per frame
    void Update()
    {

    }
}
