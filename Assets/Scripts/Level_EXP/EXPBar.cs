using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EXPBar : SerializedMonoBehaviour
{
    // ���� �ؽ�Ʈ
    [SerializeField]
    private TextMeshProUGUI _levelText;

    [SerializeField]
    private TextMeshProUGUI _expStateText;

    private void Start()
    {
        EXPObject.OnGetExp += UpdateEXPBarInfos;
    }

    // EXP Bar ���� ������ ������Ʈ
    public void UpdateEXPBarInfos()
    {
        LevelTextUpdate();
        ExpStateTextUpdate();
        EXPSliderUpdate();
    }

    // �����ؽ�Ʈ ������Ʈ
    public void LevelTextUpdate()
    {
        _levelText.text = LevelManager.Instance.ThisGameHeroLevelData.CurrentLevel.ToString();
    }

    // ����ġ ���� �ؽ�Ʈ ������Ʈ
    public void ExpStateTextUpdate()
    {
        _expStateText.text =
            $"{LevelManager.Instance.ThisGameHeroLevelData.CurrentEXP} / {DataManager.Instance.LevelDatas.LevelDataList[LevelManager.Instance.ThisGameHeroLevelData.CurrentLevel].MaxExp}";
    }

    // ����ġ �����̴� �������� ������Ʈ
    public void EXPSliderUpdate()
    {       
        GetComponent<Slider>().value = 
            (float)LevelManager.Instance.ThisGameHeroLevelData.CurrentEXP / 
            (float)DataManager.Instance.LevelDatas.LevelDataList[LevelManager.Instance.ThisGameHeroLevelData.CurrentLevel].MaxExp;
    }
}
