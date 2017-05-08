﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;
public class AnimationController : MonoBehaviour {


    public SpriteMeshAnimation faceCotroller;

    private void Start()
    {
        faceCotroller = GetComponentInChildren<SpriteMeshAnimation>();
    }


    public void SetFaceEmotion(Emotion emotion)
    {
        faceCotroller.frame = (int)emotion;
    }

}
