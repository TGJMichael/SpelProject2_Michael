using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpitBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxSpit(float spit)
    {
        slider.maxValue = spit;
        slider.value = spit;
    }

    public void SetSpit(float spit)
    {
        slider.value = spit;
    }
}
