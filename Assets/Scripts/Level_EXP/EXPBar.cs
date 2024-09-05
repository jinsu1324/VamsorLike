using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EXPBar : SerializedMonoBehaviour
{
    // 레벨 텍스트
    [SerializeField]
    private TextMeshProUGUI _levelText;

    [SerializeField]
    private TextMeshProUGUI _expStateText;

    private void Start()
    {
        EXPObject.OnGetExp += UpdateEXPBarInfos;
    }

    // EXP Bar 관련 정보들 업데이트
    public void UpdateEXPBarInfos()
    {
        LevelTextUpdate();
        ExpStateTextUpdate();
        EXPSliderUpdate();
    }

    // 레벨텍스트 업데이트
    public void LevelTextUpdate()
    {
        _levelText.text = LevelManager.Instance.ThisGameHeroLevelData.CurrentLevel.ToString();
    }

    // 경험치 상태 텍스트 업데이트
    public void ExpStateTextUpdate()
    {
        _expStateText.text =
            $"{LevelManager.Instance.ThisGameHeroLevelData.CurrentEXP} / {DataManager.Instance.LevelDatas.LevelDataList[LevelManager.Instance.ThisGameHeroLevelData.CurrentLevel].MaxExp}";
    }

    // 경험치 슬라이더 게이지바 업데이트
    public void EXPSliderUpdate()
    {       
        GetComponent<Slider>().value = 
            (float)LevelManager.Instance.ThisGameHeroLevelData.CurrentEXP / 
            (float)DataManager.Instance.LevelDatas.LevelDataList[LevelManager.Instance.ThisGameHeroLevelData.CurrentLevel].MaxExp;
    }
}
