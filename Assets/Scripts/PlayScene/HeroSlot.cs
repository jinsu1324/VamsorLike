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
    [SerializeField]
    private Button _selectButton;

    // 이 슬롯의 영웅 데이터
    private HeroData _heroData;

    // 팝업 닫힐때 사용할 액션
    private Action _popUpFinishAction;

    // 슬롯 UI 정보들 초기화
    public void InitInfoUI(HeroData heroData, Action finishAction)
    {
        _heroData = heroData;

        _heroImage.sprite = heroData.Sprite;
        _nameText.text = heroData.Name;

        // 팝업 종료될때 액션 등록
        _popUpFinishAction = finishAction;

        // 선택버튼 눌렀을때 액션 등록
        _selectButton.onClick.AddListener(OnClickSelectButton);
    }

    // 선택버튼 눌렀을때
    public void OnClickSelectButton()
    {
        // 그 영웅으로 게임시작
        PlaySceneManager.PlayStart(_heroData);

        // 팝업 종료액션 실행
        _popUpFinishAction();
    }
}
