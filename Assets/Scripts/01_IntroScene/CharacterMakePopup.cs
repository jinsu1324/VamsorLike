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
    public Action<SlotNum, CharacterData> CompleteAction;

    [SerializeField]
    private SlotNum _slotNumPopup;
    public SlotNum SlotNumPopup { get { return _slotNumPopup; } set { _slotNumPopup = value; } }

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
    
    // ĳ������ ��������Ʈ ����
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


    // ���° �̹������� �˷��ִ� number�� �����ϴ� �Լ�
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

        Debug.Log("if�� �Ȱɷ���");
        return 111;
    }

    // ���� UI �̹����� �����ϴ� �Լ�
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
        _myCharacterData.Name = _inputField.text;
        _myCharacterData.Hair = _hairImage.sprite;
        _myCharacterData.Face = _faceImage.sprite;
        _myCharacterData.Costume = _costumeImage.sprite;

        return _myCharacterData;
    }

    // ���� �̹��� ��ư
    public void OnClickNextButton(int characterPartsNum)
    {
        ChangeSprite((CharacterParts)characterPartsNum, ArrowDir.Next);
    }

    // ���� �̹��� ��ư
    public void OnClickPrevButton(int characterPartsNum)
    {
        ChangeSprite((CharacterParts)characterPartsNum, ArrowDir.Prev);
    }

    public void OnClickCompleteButton()
    {
        if (_inputField.text == "")
        {
            Debug.Log("�г����� �Է��ϼ���!");
        }            
        else
        {
            CompleteAction(_slotNumPopup, CompleteMyCharacterData());
            OnClickExitButton();
        }
    }

    public void OnClickExitButton()
    {
        gameObject.SetActive(false);
    }
}
