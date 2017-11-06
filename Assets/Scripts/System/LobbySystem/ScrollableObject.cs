using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollableObject : MonoBehaviour
{
    public GameObject canvas;

    public float minX=-7.7f;
    public float maxX=2.4f;
    public float dragSpeed=1;
    public float velocity;
    public float smooth;
    bool canClick = false;
    float clickCancelCount = 0;
    public float clickCancelDistance = 2;

    public static ScrollableObject coroutineObject;
    public static Coroutine scrollCoroutine;
    public static Coroutine forcusingCoroutine;


    private void OnMouseDrag()
    {
        if (!canvas.activeInHierarchy)
        {
            float x = Input.GetAxis("Mouse X");
            clickCancelCount += x;
            velocity = x;
            CameraController.instance.transform.position += new Vector3(x * dragSpeed, 0, 0);
            if (clickCancelCount > clickCancelDistance || clickCancelCount < -clickCancelDistance)
            {
                canClick = false;
            }
        }
    }
    private void OnMouseDown()
    {
        if(!canvas.activeInHierarchy)
        {
            ClickableObject clickableObject = GetComponent<ClickableObject>();
            if (clickableObject != null)
            {
                clickableObject.spriteRenderer.color = new Color32(150, 150, 150, 255);
                canClick = true;
                clickCancelCount = 0;
            }

            if (scrollCoroutine != null)
            {
                coroutineObject.StopCoroutine(scrollCoroutine);
            }
            if (forcusingCoroutine != null)
            {
                coroutineObject.StopCoroutine(forcusingCoroutine);
            }
        }
        

    }

    private void OnMouseUp()
    {
        if (!canvas.activeInHierarchy)
        {
            ClickableObject clickableObject = GetComponent<ClickableObject>();
            if (clickableObject != null)
            {
                clickableObject.spriteRenderer.color = new Color32(255, 255, 255, 255);
                if (canClick)
                {
                    forcusingCoroutine = StartCoroutine(Forcusing());
                    //Debug.Log(name + clickCancelCount + " Clicked!");
                }
            }

            if (scrollCoroutine != null)
            {
                coroutineObject.StopCoroutine(scrollCoroutine);
            }
            coroutineObject = this;
            scrollCoroutine = StartCoroutine(SmoothMoving());
        }  
    }
    

    IEnumerator Forcusing()
    {
        while(true)
        {
            CameraController.instance.transform.position = Vector3.Lerp(CameraController.instance.transform.position,new Vector3(transform.position.x ,CameraController.instance.transform.position.y,CameraController.instance.transform.position.z), 0.1f);
            if (CameraController.instance.transform.position.x < minX)
            {
                CameraController.instance.transform.position = new Vector3(minX, CameraController.instance.transform.position.y, CameraController.instance.transform.position.z);
                break;
            }
            if (CameraController.instance.transform.position.x > maxX)
            {
                CameraController.instance.transform.position = new Vector3(maxX, CameraController.instance.transform.position.y, CameraController.instance.transform.position.z);
                break;
            }
            if (CameraController.instance.transform.position.x - transform.position.x < 0.01f && CameraController.instance.transform.position.x - transform.position.x > -0.01f)
            {
                break;
            }
            yield return null;
        }
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
