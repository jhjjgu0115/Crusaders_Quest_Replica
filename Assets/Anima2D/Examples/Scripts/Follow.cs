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
		if(target&(Mathf.Abs(targetTransform.position.x - transform.position.x) > 0.02f))
		{
            transform.position = Vector3.Lerp(new Vector3(targetTransform.position.x,transform.position.y), transform.position, 0.1f);
		}
	}
}
