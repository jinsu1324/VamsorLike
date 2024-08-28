using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EXPBar : SerializedMonoBehaviour
{
    // 레벨 텍스트
    [SerializeField]
    private TextMeshProUGUI _levelText;


    private void Start()
    {
        EXPObject.OnGetEXP += LevelTextUpdate;
        EXPObject.OnGetEXP += EXPSliderUpdate;
    }

    // 레벨텍스트 업데이트
    public void LevelTextUpdate(EXP exp)
    {
        _levelText.text = exp.Level.ToString();
    }

    // 경험치 슬라이더 게이지바 업데이트
    public void EXPSliderUpdate(EXP exp)
    {
        GetComponent<Slider>().value = (float)exp.currentEXP / (float)exp.NextEXP;
    }
}
