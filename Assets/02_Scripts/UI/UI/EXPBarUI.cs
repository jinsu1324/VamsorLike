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
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        EXPItem.OnGetExp += Update_EXPBarInfos;
    }

    /// <summary>
    /// EXP Bar ���� ������ ������Ʈ
    /// </summary>
    public void Update_EXPBarInfos()
    {
        HeroLvExp heroLvExp = LevelManager.Instance.MyHeroLvExp;
        List<LevelData> levelDataList = DataManager.Instance.LevelDatas.LevelDataList;

        Update_LevelText(heroLvExp);
        Update_ExpStateText(heroLvExp, levelDataList);
        Update_EXPSlider(heroLvExp, levelDataList);
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
    public void Update_ExpStateText(HeroLvExp heroLvExp, List<LevelData> levelDataList)
    {
        _expStateText.text = 
            $"{heroLvExp.EXP} / " +
            $"{levelDataList[heroLvExp.Level].MaxExp}";
    }

    /// <summary>
    /// ����ġ �����̴� �������� ������Ʈ
    /// </summary>
    public void Update_EXPSlider(HeroLvExp heroLvExp, List<LevelData> levelDataList)
    {       
        GetComponent<Slider>().value = 
            (float)heroLvExp.EXP /
            (float)levelDataList[heroLvExp.Level].MaxExp;
    }

    /// <summary>
    /// �� ��ȯ�ǰų� ������Ʈ �ı��� �� �̺�Ʈ ����
    /// </summary>
    public void OnDisable()
    {
        EXPItem.OnGetExp -= Update_EXPBarInfos;
    }
}
