using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    // HP �������� ������Ʈ
    public void Update_HPSlider(float currentHp, float maxHp)
    {
        GetComponent<Slider>().value = (float)currentHp / (float)maxHp;
    }
}
