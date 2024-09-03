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

    // 선택 완료 버튼
    [SerializeField]
    private Button _selectCompleteButton;

    // 이 슬롯의 영웅 데이터
    private HeroData _HeroData;

    #region 선택 완료되었을때 해야할것들
    // 1. 팝업 닫기
    // 2. 해당 슬롯의 영웅으로 게임 시작
    // 3. 그 영웅 필드에 스폰하고 스탯도 넣어줌
    #endregion
    // 선택 완료상황일 때 호출시킬 액션
    public static event Action OnHeroSelectComplete;    


    // UI 정보들 셋팅
    public void UIInfoSetting(HeroData heroData, Action closeAction)
    {
        // 이 슬롯 영웅데이터 셋팅
        _HeroData = heroData;
        
        // UI 정보들 셋팅
        _heroImage.sprite = heroData.Sprite;
        _nameText.text = heroData.Name;

        OnHeroSelectComplete += closeAction;

        // 선택 완료 버튼 눌렀을때 호출할 함수 등록
        _selectCompleteButton.onClick.AddListener(OnClickSelectCompleteButton);
    }

    // 선택 완료 버튼 눌렀을때 호출될 함수
    public void OnClickSelectCompleteButton()
    {
        // 이 슬롯영웅의 ID를 HeroID enum 값으로 변환
        HeroID heroID = (HeroID)Enum.Parse(typeof(HeroID), _HeroData.Id);

        // 게임시작됨을 true로
        PlaySceneManager.Instance.IsGameStartTrue();

        // 이번게임영웅으로 선택된 영웅 셋팅 및 스폰
        PlaySceneManager.Instance.ThisGameHeroSetting(heroID);

        // 선택 완료상황 액션 실행 (영웅선택 팝업 닫기)
        OnHeroSelectComplete();

        // 몬스터 스폰 시작
        MonsterSpawner.Instance.StartMonsterSpawn();
    }
}
