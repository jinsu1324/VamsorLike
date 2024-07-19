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

    private Action ClickAction;

    [SerializeField]
    private TextMeshProUGUI _text;


    private void Start()
    {
        SettingSlot();
    }

    private void SettingSlot()
    {
        CharacterData characterData = CharacterDataManager.GetCharacterDataBySlot(_slotNum);
        //Debug.Log(characterData._name);

        if (characterData == null)
        {            
            ClickAction += MakeNewData;
            _text.text = "데이터 없음";
        }
        else
        {
            ClickAction += GameStart;
            _text.text = characterData._name;
        }
    }    

    private void MakeNewData()
    {        
        CharacterDataManager.MakeNewCharacterData(_slotNum);
        ClickAction = null;
        SettingSlot();
    }

    private void GameStart()
    {
        Debug.Log("게임시작!");
        CharacterDataManager.SelectSlotNum = _slotNum;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickAction();        
    }


}
