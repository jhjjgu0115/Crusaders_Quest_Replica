using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatFloat
{
    float currentValue;
    float maxValue;
    float minValue;

    FloatEvent currentValueEvent;
    FloatEvent maxValueEvent;
    FloatEvent minValueEvent;

    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            currentValue = value;
            currentValueEvent.Invoke(currentValue);
        }
    }
    public float MaxValue
    {
        get
        {
            return maxValue;
        }
        set
        {
            maxValue = value;
            maxValueEvent.Invoke(maxValue);
        }
    }
    public float MinValue
    {
        get
        {
            return minValue;
        }
        set
        {
            minValue = value;
            minValueEvent.Invoke(minValue);
        }
    }

}
