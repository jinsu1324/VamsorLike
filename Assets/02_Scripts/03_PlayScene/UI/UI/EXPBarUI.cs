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
    private TextMeshProUGUI _levelText;             // ���� �ؽ�Ʈ
    
    [SerializeField]
    private TextMeshProUGUI _expStateText;          // ����ġ ���� �ؽ�Ʈ

    /// <summary>
    /// EXP Bar ���� ������ ������Ʈ
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
    /// �����ؽ�Ʈ ������Ʈ
    /// </summary>
    public void Update_LevelText(HeroLvExp heroLvExp)
    {
        _levelText.text = heroLvExp.Level.ToString();
    }

    /// <summary>
    /// ����ġ ���� �ؽ�Ʈ ������Ʈ
    /// </summary>
    public void Update_ExpStateText(HeroLvExp heroLvExp, LevelData currentLevelData)
    {
        _expStateText.text = 
            $"{heroLvExp.EXP} / " +
            $"{currentLevelData.MaxExp}";
    }

    /// <summary>
    /// ����ġ �����̴� �������� ������Ʈ
    /// </summary>
    public void Update_EXPSlider(HeroLvExp heroLvExp, LevelData currentLevelData)
    {       
        GetComponent<Slider>().value = 
            (float)heroLvExp.EXP /
            (float)currentLevelData.MaxExp;
    }
}
