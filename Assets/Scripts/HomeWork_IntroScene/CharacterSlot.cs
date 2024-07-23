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


public class CharacterSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private SlotNum _slotNum; 

    private Action SlotClickAction;

    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private GameObject _characterGO;
    [SerializeField]
    private Image _hairImage;
    [SerializeField]
    private Image _faceImage;
    [SerializeField]
    private Image _costumeImage;




    private CharacterMakePopup _characterMakePopup;

    private IntroSceneManager _introSceneManager;


    private void Start()
    {
        _introSceneManager = FindObjectOfType<IntroSceneManager>();
        _characterMakePopup = _introSceneManager.MakeNewCharacterPopup;

        SettingSlot();        
    }

    private void SettingSlot()
    {
        CharacterData characterData = CharacterDataManager.GetCharacterDataBySlot(_slotNum);
        _characterMakePopup.ExitButtonAction += OFF_CharacterMakePopup;

        if (characterData == null)
        {
            SlotClickAction += ON_CharacterMakePopup;
            _text.text = "No Data";
            _characterGO.gameObject.SetActive(false);
        }
        else
        {
            SlotClickAction = null;
            SlotClickAction += GameStart;
            _text.text = characterData._name;
            _characterGO.gameObject.SetActive(true);
            _hairImage.sprite = characterData._hair;
            _faceImage.sprite = characterData._face;
            _costumeImage.sprite = characterData._costume;

        }
    }    

    private void MakeNewData(string nickName, Sprite hair, Sprite face, Sprite costume)
    {        
        CharacterDataManager.MakeNewCharacterData(_slotNum, nickName, hair, face, costume);

        OFF_CharacterMakePopup();
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


    public void ON_CharacterMakePopup()
    {
        _characterMakePopup._inputField.text = "";

        _characterMakePopup.gameObject.SetActive(true);        
        _characterMakePopup.CompleteButtonAction += MakeNewData;
    }

    public void OFF_CharacterMakePopup()
    {
        _characterMakePopup.gameObject.SetActive(false);
        _characterMakePopup.CompleteButtonAction = null; 
    }
}
