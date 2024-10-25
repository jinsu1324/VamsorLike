using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBarUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _bossNameText;           // 보스 이름 텍스트

    [SerializeField]
    private Slider _hpSlider;                        // HP 슬라이더


    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        BossObj.OnBossHPChanged += Refresh_BossHPBar;
    }

    /// <summary>
    /// UI 준비
    /// </summary>
    public void initialize(BossID bossID)
    {
        BossData bossData = DataManager.Instance.BossDataDict[bossID];

        _bossNameText.text = bossData.Name;
        Refresh_BossHPBar(bossData.MaxHp, bossData.MaxHp);

        gameObject.SetActive(true);
    }

    /// <summary>
    ///  HP바 슬라이더 새로고침
    /// </summary>
    public void Refresh_BossHPBar(float curHp, float maxHp)
    {
        _hpSlider.value = curHp / maxHp;
    }

    /// <summary>
    /// 씬 전환이나 오브젝트 파괴될 때 이벤트 제거
    /// </summary>
    private void OnDisable()
    {
        BossObj.OnBossHPChanged -= Refresh_BossHPBar;

        gameObject.SetActive(false);
    }
}
