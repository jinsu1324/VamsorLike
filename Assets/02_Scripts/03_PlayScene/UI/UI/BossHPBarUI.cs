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

    [SerializeField]
    private Image _delayHpImage;                      // HP 슬라이더를 따라올 딜레이 HP 이미지

    /// <summary>
    /// UI 준비
    /// </summary>
    public void initialize(BossID bossID)
    {
        BossData bossData = DataManager.Instance.BossDatas.GetDataById(bossID.ToString());

        _bossNameText.text = bossData.Name;
        _hpSlider.value = bossData.MaxHp / bossData.MaxHp;
        _delayHpImage.fillAmount = bossData.MaxHp / bossData.MaxHp;

        gameObject.SetActive(true);
    }

    /// <summary>
    ///  HP바 슬라이더 새로고침
    /// </summary>
    public void Refresh_BossHPBar(float curHp, float maxHp)
    {
        StartCoroutine(HPBarUpdate(curHp, maxHp));
    }

    /// <summary>
    /// 보스 HP 바 딜레이까지 포함 업데이트 코루틴
    /// </summary>
    private IEnumerator HPBarUpdate(float curHp, float maxHp)
    {
        _hpSlider.value = curHp / maxHp;

        while (Mathf.Abs(_delayHpImage.fillAmount - _hpSlider.value) > 0.01)
        {
            _delayHpImage.fillAmount = Mathf.Lerp(_delayHpImage.fillAmount, _hpSlider.value, 3 * Time.deltaTime);
            yield return null;
        }

        _delayHpImage.fillAmount = _hpSlider.value;
    }

    /// <summary>
    /// 팝업 끄기
    /// </summary>
    public void Popup_OFF()
    {
        gameObject.SetActive(false);
    }
}
