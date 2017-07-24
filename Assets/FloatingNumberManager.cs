using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class FloatingNumberManager : MonoBehaviour
{
    public GameObject floatingObject;

    public static FloatingNumberManager instance = null;
    public static FloatingNumberManager Instance
    {
        get
        {
            if(instance)
            {
                return instance;
            }
            else
            {
                instance = FindObjectOfType<FloatingNumberManager>();
                if (!instance)
                {
                    GameObject container = new GameObject();
                    container.name = "FloatingNumberManager";
                    instance = container.AddComponent<FloatingNumberManager>();
                }
                return instance;
            }
        }
    }

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
        else if(instance !=this)
        {
            Destroy(gameObject);
        }
    }

    public static void FloatingNumber(float number)
    {

    }
}
