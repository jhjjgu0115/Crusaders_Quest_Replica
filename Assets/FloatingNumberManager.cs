using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public  class FloatingNumberManager : MonoBehaviour
{
    public static GameObject floatingObject;
    public static Canvas floatingArea;

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
        floatingObject = Resources.Load<GameObject>("Prefabs/FloatingNumberObject");
        foreach(GameObject gamaobject in FindObjectsOfType<GameObject>())
        {
            if(gamaobject.name== "FloatingArea")
            {
                floatingArea = gamaobject.GetComponent<Canvas>();
            }
        }
    }
    private void Start()
    {
    }
    public static void FloatingNumber(GameObject target,float number,E_FloatingType floatingType)
    {
        GameObject floatingNumber = Instantiate<GameObject>(floatingObject);
        floatingNumber.transform.SetParent(floatingArea.transform);
        Text floatingText = floatingNumber.GetComponent<Text>();
        floatingText.text = ((int)number).ToString();
        floatingNumber.transform.position = target.transform.position + new Vector3(Random.value-0.5f,1.2f + Random.value, 0);

        switch(floatingType)
        {
            case E_FloatingType.NonpenetratingDamage:
                {
                    floatingText.color = Color.gray;
                    break;
                }
            case E_FloatingType.FullPenetrationgDamage:
                {
                    floatingText.color = Color.yellow;
                    break;
                }
            case E_FloatingType.CriticalDamage:
                {
                    floatingText.color = Color.red;
                    break;
                }
            case E_FloatingType.Heal:
                {
                    floatingText.color = Color.green;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
