using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSlot : SerializedMonoBehaviour
{
    // ui 에 표시될 영웅 정보들
    [SerializeField]
    private Image _heroImage;
    [SerializeField]
    private TextMeshProUGUI _nameText;

    // 선택 완료 버튼
    [SerializeField]
    private Button _selectCompleteButton;

    // 이 슬롯의 영웅 데이터
    private HeroData _HeroData;

    // 선택 완료상황일 때 호출시킬 액션
    private Action _selectCompleteAction;

    // UI 정보들 셋팅
    public void UIInfoSetting(HeroData heroData, Action selectCompleteAction)
    {
        // 이 슬롯 영웅데이터 셋팅
        _HeroData = heroData;
        
        // UI 정보들 셋팅
        _heroImage.sprite = heroData.Sprite;
        _nameText.text = heroData.Name;

        // 선택 완료 상황일 때 호출할 액션에 함수 등록 (현재 팝업 닫기가 들어가있음)
        _selectCompleteAction = selectCompleteAction;

        // 선택 완료 버튼 눌렀을때 호출할 함수 등록
        _selectCompleteButton.onClick.AddListener(OnClickSelectCompleteButton);
    }

    // 선택 완료 버튼 눌렀을때 호출될 함수
    public void OnClickSelectCompleteButton()
    {
        // 이 슬롯영웅의 ID를 HeroID enum 값으로 변환
        HeroID heroID = (HeroID)Enum.Parse(typeof(HeroID), _HeroData.Id);

        // 이 슬롯의 영웅으로 게임시작
        PlaySceneManager.Instance.PlayStart(heroID);

        // 선택 완료상황 액션 실행
        _selectCompleteAction();
    }
}
