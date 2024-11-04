using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroLvExp
{
    public int EXP;
    public int Level;

    public HeroLvExp(int exp, int level)
    {
        EXP = exp;
        Level = level;
    }
}

public class LevelManager : SerializedMonoBehaviour
{
    // 이번게임 영웅 레벨 경험치
    public HeroLvExp MyHeroLvExp { get; set; } = new HeroLvExp(0, 0);
        

    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        PlaySceneCanvas.Instance.EXPBarUI.Update_EXPBarInfos();
    }

    /// <summary>
    /// 경험치 증가
    /// </summary>
    public void EXPUp(int amount)
    {

        MyHeroLvExp.EXP += amount;
        RefreshEXPBarUI();

        List<LevelData> levelDataList = DataManager.Instance.LevelDatas.DataList;

        if (MyHeroLvExp.EXP >= levelDataList[MyHeroLvExp.Level].MaxExp)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// EXP 바 UI 새로고침
    /// </summary>
    public void RefreshEXPBarUI()
    {
        PlaySceneCanvas.Instance.EXPBarUI.Update_EXPBarInfos();
    }

    /// <summary>
    /// 레벨 증가
    /// </summary>
    private void LevelUp()
    {
        MyHeroLvExp.Level++;
        MyHeroLvExp.EXP -= 100; 

        PlaySceneCanvas.Instance.EXPBarUI.Update_EXPBarInfos();
        PlaySceneCanvas.Instance.SkillSelectPopup.OpenPopup();
    }





    // 치트
    public void OnClickExpUpCheatButton()
    {
        EXPUp(10);
        PlaySceneCanvas.Instance.EXPBarUI.Update_EXPBarInfos();
    }
}
