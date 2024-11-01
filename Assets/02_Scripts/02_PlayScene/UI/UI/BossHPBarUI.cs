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
    /// UI 준비
    /// </summary>
    public void initialize(BossID bossID)
    {

        // 수정필요!

        //BossData bossData = DataManager.Instance.BossDataDict[bossID];
                
        //_bossNameText.text = bossData.Name;
        //Refresh_BossHPBar(bossData.MaxHp, bossData.MaxHp);

        //gameObject.SetActive(true);
    }

    /// <summary>
    ///  HP바 슬라이더 새로고침
    /// </summary>
    public void Refresh_BossHPBar(float curHp, float maxHp)
    {
        _hpSlider.value = curHp / maxHp;
    }

    /// <summary>
    /// 팝업 끄기
    /// </summary>
    public void Popup_OFF()
    {
        gameObject.SetActive(false);
    }
}
