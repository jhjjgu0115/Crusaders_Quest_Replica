using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTouch : MonoBehaviour
{
    public float dragSpeed=1;
    Camera mainCamera;
    public float velocity;
    public float smooth;
    private void OnMouseDrag()
    {
        float x = Input.GetAxis("Mouse X");
        velocity = x;
        mainCamera.transform.position += new Vector3(x* dragSpeed, 0, 0);
        

    }

    private void OnMouseDown()
    {
        StopCoroutine(SmoothMoving());
    }

    private void OnMouseUp()
    {
        StartCoroutine(SmoothMoving());
    }
    
    IEnumerator SmoothMoving()
    {
        while(true)
        {
            mainCamera.transform.position += new Vector3(velocity * dragSpeed, 0, 0);
            velocity *= smooth;
            if(velocity>0.01f || velocity < -0.01f)
            {
            }
            else
            {
                break;
            }
            Debug.Log(1);
            yield return null;
        }
    }


    // Use this for initialization
    void Start ()
    {
        mainCamera = FindObjectOfType<Camera>();
	}



    public float minX;
    public float maxX;


    // Update is called once per frame
    void Update()
    {

        if (mainCamera.transform.position.x < minX)
        {
            StopAllCoroutines();
            mainCamera.transform.position = new Vector3(minX, mainCamera.transform.position.y);
        }
        else if (mainCamera.transform.position.x > maxX)
        {

            StopAllCoroutines();
            mainCamera.transform.position = new Vector3(maxX, mainCamera.transform.position.y);
        }

    }
}
