using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Follow : MonoBehaviour
{
	public Transform target;
    Transform targetTransform;
	public Vector3 offset;

    private void Start()
    {
        targetTransform = target.transform;
    }

    void LateUpdate()
	{
		if(target&(Vector3.Distance(targetTransform.position,transform.position)>1.0f))
		{
            transform.position = Vector3.Lerp(targetTransform.position, transform.position, 0.5f);
			transform.position = target.position + offset;
		}
	}
}
