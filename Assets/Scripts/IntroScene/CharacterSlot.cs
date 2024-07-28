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


public enum SlotNum
{
    First,
    Second,
    Third
}

public class CharacterSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private SlotNum _slotNum; 

    private Action SlotClickAction;

    [SerializeField]
    private TextMeshProUGUI _text;

    private IntroSceneManager _introSceneManager;

    private void Start()
    {
        _introSceneManager = FindObjectOfType<IntroSceneManager>();

        _introSceneManager.CharacterMakePopup.CompleteAction += MakeNewData;

        SettingSlot();        
    }

    private void SettingSlot()
    {
        CharacterData characterData_Slot = CharacterDataManager.GetCharacterDataBySlot(_slotNum);

        if (characterData_Slot == null)
        {
            _text.text = "No Data";
            SlotClickAction += CharacterMakePopupON;
        }
        else
        {
            SlotClickAction = null;
            SlotClickAction += GameStart;
            _text.text = characterData_Slot._name;

        }
    }
    public void CharacterMakePopupON()
    {
        _introSceneManager.CharacterMakePopup.gameObject.SetActive(true);
        _introSceneManager.CharacterMakePopup.SlotNum_Popup = _slotNum;
    }

    private void MakeNewData(SlotNum slotNum, CharacterData myCharacterData)
    {
        CharacterDataManager.MakeNewCharacterData(slotNum, myCharacterData);
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
}
