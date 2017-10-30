using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HeroButton : MonoBehaviour {

    public Image heroImage;
    public Image backGroundImage;
    public RectTransform heroImageRect;
    public Sprite noneSelectButtonImage;
    public Sprite selectedButtonImage;
    public Toggle toggle;


    // Use this for initialization
    void Start ()
    {
        toggle = GetComponent<Toggle>();
        backGroundImage = GetComponent<Image>();
        heroImage.SetNativeSize();
        Vector2 size = heroImageRect.sizeDelta;
        Vector2 pixelPivot = heroImage.sprite.pivot;
        Vector2 percentPivot = new Vector2(pixelPivot.x / size.x, pixelPivot.y / size.y);
        heroImageRect.pivot = percentPivot;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ChangeSprite()
    {
        if(toggle.isOn)
        {
            backGroundImage.sprite = selectedButtonImage;
        }
        else
        {
            backGroundImage.sprite = noneSelectButtonImage;
        }
    }
}
