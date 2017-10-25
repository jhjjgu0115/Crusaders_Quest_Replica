using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour {
    SpriteRenderer spriteRenderer;

    private void OnMouseDown()
    {
        spriteRenderer.color = new Color32(150, 150, 150, 255);
    }
    private void OnMouseUp()
    {
        spriteRenderer.color = new Color32 (255, 255, 255,255);
    }

    // Use this for initialization
    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update ()
    {
    }
}
