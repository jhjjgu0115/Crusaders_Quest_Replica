using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCodeBlock : MonoBehaviour {

    public GameObject target; 


    private void OnMouseDown()
    {
        StartCoroutine(DropDown());

    }
    IEnumerator DropDown()
    {
        Transform targetTransform = target.transform;
        while (Vector3.Distance(transform.position, target.transform.position) >= 0.4f)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, 0.4f);
            yield return null;
        }
        transform.position = target.transform.position;
        yield return null;
    }
}
