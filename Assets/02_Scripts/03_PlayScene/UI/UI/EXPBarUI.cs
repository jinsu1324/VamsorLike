using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EXPBarUI : SerializedMonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _levelText;             // 레벨 텍스트
    
    [SerializeField]
    private TextMeshProUGUI _expStateText;          // 경험치 상태 텍스트

    /// <summary>
    /// EXP Bar 관련 정보들 업데이트
    /// </summary>
    public void Update_EXPBarInfos()
    {
        HeroLvExp heroLvExp = PlaySceneManager.Instance.LevelManager.MyHeroLvExp;
        LevelData currentLevelData = DataManager.Instance.LevelDatas.GetDataById(heroLvExp.Level.ToString());

        Update_LevelText(heroLvExp);
        Update_ExpStateText(heroLvExp, currentLevelData);
        Update_EXPSlider(heroLvExp, currentLevelData);
    }

    /// <summary>
    /// 레벨텍스트 업데이트
    /// </summary>
    public void Update_LevelText(HeroLvExp heroLvExp)
    {
        _levelText.text = heroLvExp.Level.ToString();
    }

    /// <summary>
    /// 경험치 상태 텍스트 업데이트
    /// </summary>
    public void Update_ExpStateText(HeroLvExp heroLvExp, LevelData currentLevelData)
    {
        _expStateText.text = 
            $"{heroLvExp.EXP} / " +
            $"{currentLevelData.MaxExp}";
    }

    /// <summary>
    /// 경험치 슬라이더 게이지바 업데이트
    /// </summary>
    public void Update_EXPSlider(HeroLvExp heroLvExp, LevelData currentLevelData)
    {       
        GetComponent<Slider>().value = 
            (float)heroLvExp.EXP /
            (float)currentLevelData.MaxExp;
    }
}
