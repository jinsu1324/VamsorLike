using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EXPBar : SerializedMonoBehaviour
{
    // ���� �ؽ�Ʈ
    [SerializeField]
    private TextMeshProUGUI _levelText;


    private void Start()
    {
        EXPObject.OnGetEXP += LevelTextUpdate;
        EXPObject.OnGetEXP += EXPSliderUpdate;
    }

    // �����ؽ�Ʈ ������Ʈ
    public void LevelTextUpdate(EXP exp)
    {
        _levelText.text = exp.Level.ToString();
    }

    // ����ġ �����̴� �������� ������Ʈ
    public void EXPSliderUpdate(EXP exp)
    {
        GetComponent<Slider>().value = (float)exp.currentEXP / (float)exp.NextEXP;
    }
}
