using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.U2D.Animation;

public enum SLOTNUM
{
    First,
    Second,
    Third
}

public class CharacterSlot : MonoBehaviour, IPointerClickHandler
{
    // 슬롯 번호
    public SLOTNUM SlotNum { get; set; }

    // 슬롯 캐릭터 데이터
    public CharacterData SlotCharacterData { get; set; }

    // ui 텍스트
    [SerializeField]
    private TextMeshProUGUI _uiText;

    // 슬롯 클릭했을 때 액션
    private Action _slotClickAction;


    // 슬롯 클릭했을때 호출
    public void OnPointerClick(PointerEventData eventData)
    {
        _slotClickAction();
    }

    // 슬롯 넘버 각자에 맞게 초기화 해주는 함수
    public void SlotNumInit(SLOTNUM slotNum)
    {
        this.SlotNum = slotNum;
    }

    // 슬롯 캐릭터 데이터 초기화해주는 함수
    public void SlotCharacterDataInit(SLOTNUM slotNum)
    {
        IntroSceneManager.Instance.CharacterDataManager.LoadCharacterData();
        SlotCharacterData = IntroSceneManager.Instance.CharacterDataManager.GetCharacterDataBySlot(slotNum);

        DataCheckAndChoiceNextAction();
    }

    // 데이터를 체크하고, 다음액션을 미리 정해서 액션에 넣어주는 함수 (생성창을 열지, 게임을 시작할지)
    public void DataCheckAndChoiceNextAction()
    {
        // 데이터가 없으면, 다음액션에 캐릭터생성창을 열도록
        if (SlotCharacterData == null)
        {
            _slotClickAction = OpenCharacterMakePopup;
            _uiText.text = "No Data";
        }
        // 데이터가 있으면, 다음액션에 게임시작을 하도록
        else if (SlotCharacterData != null)
        {
            _slotClickAction = GameStart;
            _uiText.text = $"{SlotCharacterData.Name}";
        }
    }

    // 캐릭터생성 팝업 ON (캐릭터생성팝업에게 열라고 명령)
    public void OpenCharacterMakePopup()
    {
        Debug.Log("캐릭터 생성팝업을 엽니다.");
        IntroSceneManager.Instance.CharacterMakePopup.OpenPopup(SlotNum);
    }

    // 게임 시작
    private void GameStart()
    {
        Debug.Log("게임을 시작합니다.");
    }   
}
