using DG.Tweening;
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
    private Slider _expSlider;                      // EXP 슬라이더

    [SerializeField]
    private TextMeshProUGUI _levelText;             // 레벨 텍스트

    [SerializeField]
    private Image _blinkImage;                      // 경험치 얻었을 때 깜빡일 이미지
    
    private float _duration = 0.2f;                 // 깜빡일 시간

    /// <summary>
    /// EXP Bar 관련 정보들 업데이트
    /// </summary>
    public void Update_EXPBarInfos()
    {
        HeroLvExp heroLvExp = PlaySceneManager.Instance.LevelManager.MyHeroLvExp;
        LevelData currentLevelData = DataManager.Instance.LevelDatas.GetDataById(heroLvExp.Level.ToString());

        Update_LevelText(heroLvExp);
        Update_EXPSlider(heroLvExp, currentLevelData);
    }

    /// <summary>
    /// 레벨텍스트 업데이트
    /// </summary>
    public void Update_LevelText(HeroLvExp heroLvExp)
    {
        _levelText.text = $"Lv.{heroLvExp.Level.ToString()}";
    }
        

    /// <summary>
    /// 경험치 슬라이더 게이지바 업데이트
    /// </summary>
    public void Update_EXPSlider(HeroLvExp heroLvExp, LevelData currentLevelData)
    {
        BlinkSlider();
        _expSlider.value = 
            (float)heroLvExp.EXP /
            (float)currentLevelData.MaxExp;
    }

    /// <summary>
    /// 슬라이더 깜빡이기
    /// </summary>
    private void BlinkSlider()
    {
        // 알파 값 0에서 1로, 다시 1에서 0으로 돌아오는 애니메이션을 1회만 실행
        _blinkImage.
            DOFade(1, _duration).
            OnComplete(() => { _blinkImage.DOFade(0, _duration); });
    }
}
