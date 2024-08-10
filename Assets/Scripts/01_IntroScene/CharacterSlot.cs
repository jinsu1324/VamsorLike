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

    private Action _SlotClickAction;

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
        CharacterData characterDataSlot = CharacterDataManager.GetCharacterDataBySlot(_slotNum);

        if (characterDataSlot == null)
        {
            _text.text = "No Data";
            _SlotClickAction += CharacterMakePopupON;
        }
        else
        {
            _SlotClickAction = null;
            _SlotClickAction += GameStart;
            _text.text = characterDataSlot.Name;

        }
    }
    public void CharacterMakePopupON()
    {
        _introSceneManager.CharacterMakePopup.gameObject.SetActive(true);
        _introSceneManager.CharacterMakePopup.SlotNumPopup = _slotNum;
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
        _SlotClickAction();        
    }
}
