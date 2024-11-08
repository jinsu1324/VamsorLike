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
    private Slider _expSlider;                      // EXP �����̴�

    [SerializeField]
    private TextMeshProUGUI _levelText;             // ���� �ؽ�Ʈ

    /// <summary>
    /// EXP Bar ���� ������ ������Ʈ
    /// </summary>
    public void Update_EXPBarInfos()
    {
        HeroLvExp heroLvExp = PlaySceneManager.Instance.LevelManager.MyHeroLvExp;
        LevelData currentLevelData = DataManager.Instance.LevelDatas.GetDataById(heroLvExp.Level.ToString());

        Update_LevelText(heroLvExp);
        Update_EXPSlider(heroLvExp, currentLevelData);
    }

    /// <summary>
    /// �����ؽ�Ʈ ������Ʈ
    /// </summary>
    public void Update_LevelText(HeroLvExp heroLvExp)
    {
        _levelText.text = $"Lv.{heroLvExp.Level.ToString()}";
    }
        

    /// <summary>
    /// ����ġ �����̴� �������� ������Ʈ
    /// </summary>
    public void Update_EXPSlider(HeroLvExp heroLvExp, LevelData currentLevelData)
    {
        _expSlider.value = 
            (float)heroLvExp.EXP /
            (float)currentLevelData.MaxExp;
    }
}
