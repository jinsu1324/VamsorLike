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

    [SerializeField]
    private Image _delayHpImage;                      // HP �����̴��� ����� ������ HP �̹���

    /// <summary>
    /// UI �غ�
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
    ///  HP�� �����̴� ���ΰ�ħ
    /// </summary>
    public void Refresh_BossHPBar(float curHp, float maxHp)
    {
        StartCoroutine(HPBarUpdate(curHp, maxHp));
    }

    /// <summary>
    /// ���� HP �� �����̱��� ���� ������Ʈ �ڷ�ƾ
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
    /// �˾� ����
    /// </summary>
    public void Popup_OFF()
    {
        gameObject.SetActive(false);
    }
}
