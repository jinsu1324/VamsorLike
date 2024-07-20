using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.EventSystems;


public class CharacterSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private SlotNum _slotNum; 

    private Action SlotClickAction;

    [SerializeField]
    private TextMeshProUGUI _text;
   
    private MakeCharacterPopup _makeNewCharacterPopup;

    private IntroSceneManager _introSceneManager;


    private void Start()
    {
        _introSceneManager = FindObjectOfType<IntroSceneManager>();
        _makeNewCharacterPopup = _introSceneManager.MakeNewCharacterPopup;

        SettingSlot();
    }

    private void SettingSlot()
    {
        CharacterData characterData = CharacterDataManager.GetCharacterDataBySlot(_slotNum);

        if (characterData == null)
        {
            SlotClickAction += ON_MakeCharacterPopup;
            _text.text = "No Data";
        }
        else
        {
            SlotClickAction += GameStart;
            _text.text = characterData._name;
        }
    }    

    private void MakeNewData(string nickName)
    {        
        CharacterDataManager.MakeNewCharacterData(_slotNum, nickName);        
        _makeNewCharacterPopup.CompleteButtonAction = null;        
        _makeNewCharacterPopup.gameObject.SetActive(false);
        SettingSlot();
    }

    private void GameStart()
    {
        Debug.Log("Game Start!");
        CharacterDataManager.SelectSlotNum = _slotNum;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SlotClickAction();        
    }


    public void ON_MakeCharacterPopup()
    {        
        _makeNewCharacterPopup.gameObject.SetActive(true);
        _makeNewCharacterPopup._titleText.text = $"{(int)_slotNum + 1}번 슬롯 캐릭터 생성화면";

        _makeNewCharacterPopup.CompleteButtonAction += MakeNewData;
        SlotClickAction = null;
    }

}
