using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

// 영웅 슬롯 : 영웅 정보들 / 영웅 선택 완료 확정 및 처리 (게임시작 명령, 팝업 닫기 액션)
public class HeroSlot : SerializedMonoBehaviour
{
    // ui 에 표시될 영웅 정보들
    [SerializeField]
    private Image _heroImage;
    [SerializeField]
    private TextMeshProUGUI _nameText;
    [SerializeField]
    private TextMeshProUGUI _descText;

    // 선택 완료 버튼
    [SerializeField]
    private Button _selectCompleteButton;

    // 이 슬롯의 영웅 데이터
    private HeroData _HeroData;

    // 선택 완료상황일 때 호출시킬 액션
    private Action _onSelectFinish;    


    // UI 정보들 셋팅
    public void UIInfoSetting(HeroData heroData, Action popupClose)
    {
        // 이 슬롯 영웅데이터 셋팅
        _HeroData = heroData;
        
        // UI 정보들 셋팅
        _heroImage.sprite = heroData.Sprite;
        _nameText.text = heroData.Name;
        _descText.text = heroData.Desc;

        _onSelectFinish += popupClose;

        // 선택 완료 버튼 눌렀을때 호출할 함수 등록
        _selectCompleteButton.onClick.AddListener(OnClickSelectCompleteButton);
    }

    // 선택 완료 버튼 눌렀을때 호출될 함수
    public void OnClickSelectCompleteButton()
    {
        // 이 슬롯영웅의 ID를 HeroID enum 값으로 변환
        HeroID heroID = (HeroID)Enum.Parse(typeof(HeroID), _HeroData.Id);

        // 이번게임영웅으로 선택된 영웅 셋팅 및 스폰
        PlaySceneManager.Instance.ThisGameHeroSetting(heroID);

        // 카메라 따라다닐 타겟 설정
        Camera.main.GetComponent<CameraFollow>().SetFollowTarget(PlaySceneManager.ThisGameHeroObject);

        // 스킬 선택팝업 ON
        PlaySceneManager.Instance.PlaySceneCanvas.SkillChoicePopupUI.OpenSkillPopup();

        // 영웅선택 팝업 닫기
        _onSelectFinish();
    }
}
