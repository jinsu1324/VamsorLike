using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ArrowDir
{
    Next,
    Prev
}


public class CharacterMakePopup : MonoBehaviour
{
    [SerializeField]
    private SlotNum _slotNum_Popup;
    public SlotNum SlotNum_Popup { get { return _slotNum_Popup; } set { _slotNum_Popup = value; } }


    public Action<SlotNum, CharacterData> CompleteAction;

    [SerializeField]
    private TMP_InputField _inputField;

    [SerializeField]
    private Image _hairImage;
    [SerializeField]
    private Image _faceImage;
    [SerializeField]
    private Image _costumeImage;

    private int _hairNum;
    private int _faceNum;
    private int _costumeNum;

    private CharacterData _myCharacterData = new CharacterData();

    private void OnEnable()
    {
        AllNumInit();
        AllUIImageInit();
    }    
    
    private void ChangeSprite(CharacterParts characterParts, ArrowDir arrowDir)
    {
        List<Sprite> spriteList = new List<Sprite>();

        if (characterParts == CharacterParts.Hair)
        {
            spriteList = CharacterResourcesManager.Instance.HairList;
            ChangeUIImage(_hairImage, spriteList, ChangeNum(ref _hairNum, spriteList.Count, arrowDir));
        }
        else if (characterParts == CharacterParts.Face)
        {
            spriteList = CharacterResourcesManager.Instance.FaceList;
            ChangeUIImage(_faceImage, spriteList, ChangeNum(ref _faceNum, spriteList.Count, arrowDir));
        }
        else if (characterParts == CharacterParts.Costume)
        {
            spriteList = CharacterResourcesManager.Instance.CostumeList;
            ChangeUIImage(_costumeImage, spriteList, ChangeNum(ref _costumeNum, spriteList.Count, arrowDir));        
        }
    }

    private int ChangeNum(ref int num, int listCount, ArrowDir arrowDir)
    {
        if (arrowDir == ArrowDir.Next)
        {
            num++;

            if (num >= listCount)
                num = 0;

            return num;
        }
        else if (arrowDir == ArrowDir.Prev)
        {
            num--;

            if (num < 0)
                num = listCount - 1;

            return num;
        }


        Debug.Log("if에 안걸러짐");
        return 111;
    }

    private void ChangeUIImage(Image targetUIImage, List<Sprite> spriteList, int currentNum)
    {
        targetUIImage.sprite = spriteList[currentNum];
    }

    private void AllNumInit()
    {
        _hairNum = 0;
        _faceNum = 0;
        _costumeNum = 0;
    }

    private void AllUIImageInit()
    {
        _hairImage.sprite = CharacterResourcesManager.Instance.HairList[_hairNum];
        _faceImage.sprite = CharacterResourcesManager.Instance.FaceList[_faceNum];
        _costumeImage.sprite = CharacterResourcesManager.Instance.CostumeList[_costumeNum];
    }

    private CharacterData CompleteMyCharacterData()
    {
        _myCharacterData._name = _inputField.text;
        _myCharacterData._hair = _hairImage.sprite;
        _myCharacterData._face = _faceImage.sprite;
        _myCharacterData._costume = _costumeImage.sprite;

        return _myCharacterData;
    }

    public void OnClickNextButton(int characterPartsNum)
    {
        ChangeSprite((CharacterParts)characterPartsNum, ArrowDir.Next);
    }

    public void OnClickPrevButton(int characterPartsNum)
    {
        ChangeSprite((CharacterParts)characterPartsNum, ArrowDir.Prev);
    }

    public void OnClickCompleteButton()
    {
        if (_inputField.text == "")
        {
            Debug.Log("닉네임을 입력하세요!");
        }            
        else
        {
            CompleteAction(_slotNum_Popup, CompleteMyCharacterData());
            OnClickExitButton();
        }
    }

    public void OnClickExitButton()
    {
        gameObject.SetActive(false);
    }
}
