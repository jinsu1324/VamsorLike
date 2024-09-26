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

    // 경험치 상태 텍스트
    [SerializeField]
    private TextMeshProUGUI _expStateText;

    private void Start()
    {
        EXPObject.OnGetExp += Update_EXPBarInfos;
    }

    // EXP Bar 관련 정보들 업데이트
    public void Update_EXPBarInfos()
    {
        HeroLvExp heroLvExp = LevelManager.Instance.MyHeroLvExp;
        List<LevelData> levelDataList = LevelManager.Instance.LevelDatas.LevelDataList;

        Update_LevelText(heroLvExp);
        Update_ExpStateText(heroLvExp, levelDataList);
        Update_EXPSlider(heroLvExp, levelDataList);
    }

    // 레벨텍스트 업데이트
    public void Update_LevelText(HeroLvExp heroLvExp)
    {
        _levelText.text = heroLvExp.Level.ToString();
    }

    // 경험치 상태 텍스트 업데이트
    public void Update_ExpStateText(HeroLvExp heroLvExp, List<LevelData> levelDataList)
    {
        _expStateText.text = 
            $"{heroLvExp.EXP} / " +
            $"{levelDataList[heroLvExp.Level].MaxExp}";
    }

    // 경험치 슬라이더 게이지바 업데이트
    public void Update_EXPSlider(HeroLvExp heroLvExp, List<LevelData> levelDataList)
    {       
        GetComponent<Slider>().value = 
            (float)heroLvExp.EXP /
            (float)levelDataList[heroLvExp.Level].MaxExp;
    }
}
