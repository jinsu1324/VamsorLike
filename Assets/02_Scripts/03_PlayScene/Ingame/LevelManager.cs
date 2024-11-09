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
    // �̹����� ���� ���� ����ġ
    public HeroLvExp MyHeroLvExp { get; set; } = new HeroLvExp(0, 1);
        

    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        PlaySceneCanvas.Instance.EXPBarUI.Update_EXPBarInfos();
    }

    /// <summary>
    /// ����ġ ����
    /// </summary>
    public void EXPUp(int amount)
    {
        MyHeroLvExp.EXP += amount;
        RefreshEXPBarUI();

        LevelData currentLevelData = DataManager.Instance.LevelDatas.GetDataById(MyHeroLvExp.Level.ToString());

        if (MyHeroLvExp.EXP >= currentLevelData.MaxExp)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// EXP �� UI ���ΰ�ħ
    /// </summary>
    public void RefreshEXPBarUI()
    {
        PlaySceneCanvas.Instance.EXPBarUI.Update_EXPBarInfos();
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void LevelUp()
    {
        MyHeroLvExp.Level++;
        MyHeroLvExp.EXP -= 100; 

        PlaySceneCanvas.Instance.EXPBarUI.Update_EXPBarInfos();
        PlaySceneCanvas.Instance.SkillSelectPopup.OpenPopup();
    }





    // ġƮ
    public void OnClickExpUpCheatButton()
    {
        EXPUp(100);
        PlaySceneCanvas.Instance.EXPBarUI.Update_EXPBarInfos();
    }
}
