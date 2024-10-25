using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBarUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _bossNameText;           // ���� �̸� �ؽ�Ʈ

    [SerializeField]
    private Slider _hpSlider;                        // HP �����̴�


    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        BossObj.OnBossHPChanged += Refresh_BossHPBar;
    }

    /// <summary>
    /// UI �غ�
    /// </summary>
    public void initialize(BossID bossID)
    {
        BossData bossData = DataManager.Instance.BossDataDict[bossID];

        _bossNameText.text = bossData.Name;
        Refresh_BossHPBar(bossData.MaxHp, bossData.MaxHp);

        gameObject.SetActive(true);
    }

    /// <summary>
    ///  HP�� �����̴� ���ΰ�ħ
    /// </summary>
    public void Refresh_BossHPBar(float curHp, float maxHp)
    {
        _hpSlider.value = curHp / maxHp;
    }

    /// <summary>
    /// �� ��ȯ�̳� ������Ʈ �ı��� �� �̺�Ʈ ����
    /// </summary>
    private void OnDisable()
    {
        BossObj.OnBossHPChanged -= Refresh_BossHPBar;

        gameObject.SetActive(false);
    }
}
