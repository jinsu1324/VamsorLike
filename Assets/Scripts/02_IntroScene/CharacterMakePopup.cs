using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ARROWDIR
{
    Next,
    Prev
}

public class CharacterMakePopup : SerializedMonoBehaviour
{
    // UI �̹�����
    [Title("UI Images", bold: false)]
    [SerializeField]
    private Image _hairImage;
    [SerializeField]
    private Image _faceImage;
    [SerializeField]
    private Image _costumeImage;

    // �г��� ��ǲ�ʵ�
    [Title("")]
    [SerializeField]
    private TMP_InputField _nickNameinputField;

    // ������� �����ؼ� �� �˾�����
    private SLOTNUM _selectedSlotNum;

    // ���� ���õǾ��ִ� ���� �ѹ�
    private int _curHairNum;
    private int _curFaceNum;
    private int _curCostumeNum;

    // �Ϸ�� ĳ���� ������
    private CharacterData _completeCharacterData = new CharacterData();


    // �˾� ������ ȣ��
    public void OpenPopup(SLOTNUM slotNum)
    {        
        _selectedSlotNum = slotNum;

        AllCurNumInit();
        AllUIImageInit();

        this.gameObject.SetActive(true);
    }    

    // Next��ư ������ ȣ��
    public void OnClickNextButton(int characterPartsNum)
    {
        // ���� �̹����� ����
        ChangeSprite((CHARACTERPARTS)characterPartsNum, ARROWDIR.Next);
    }

    // Prev��ư ������ ȣ��
    public void OnClickPrevButton(int characterPartsNum)
    {
        // ���� �̹����� ����
        ChangeSprite((CHARACTERPARTS)characterPartsNum, ARROWDIR.Prev);
    }

    // Complete��ư ������ ȣ��
    public void OnClickCompleteButton()
    {
        if (_nickNameinputField.text == "")
        {
            Debug.Log("�г����� �Է��ϼ���!");
        }
        else
        {
            IntroSceneManager.Instance.CharacterDataManager.MakeNewCharacterData(_selectedSlotNum, CompleteMyCharacterData()); 
            IntroSceneManager.Instance.CharacterSlotManager.InitSlots();
            OnClickExitButton();
        }
    }

    // Exit��ư ������ ȣ��
    public void OnClickExitButton()
    {
        gameObject.SetActive(false);
    }

    // ��� �̹���Num 0���� �ʱ�ȭ
    private void AllCurNumInit()
    {
        _curHairNum = 0;
        _curFaceNum = 0;
        _curCostumeNum = 0;
    }

    // ��� UI�̹��� 0��° �̹����� �ʱ�ȭ
    private void AllUIImageInit()
    {
        _hairImage.sprite = IntroSceneManager.Instance.CharacterResourcesManager.HairList[_curHairNum];
        _faceImage.sprite = IntroSceneManager.Instance.CharacterResourcesManager.FaceList[_curFaceNum];
        _costumeImage.sprite = IntroSceneManager.Instance.CharacterResourcesManager.CostumeList[_curCostumeNum];
    }

    // ĳ������ ��������Ʈ ����
    private void ChangeSprite(CHARACTERPARTS characterParts, ARROWDIR arrowDir)
    {
        List<Sprite> spriteList = new List<Sprite>();
        int curNum = 0;

        if (characterParts == CHARACTERPARTS.Hair)
        {
            spriteList = IntroSceneManager.Instance.CharacterResourcesManager.HairList;
            curNum = ChangeCurNum(ref _curHairNum, spriteList.Count, arrowDir);
            ChangeUIImage(_hairImage, spriteList, curNum);
        }
        else if (characterParts == CHARACTERPARTS.Face)
        {
            spriteList = IntroSceneManager.Instance.CharacterResourcesManager.FaceList;
            curNum = ChangeCurNum(ref _curFaceNum, spriteList.Count, arrowDir);
            ChangeUIImage(_faceImage, spriteList, curNum);
        }
        else if (characterParts == CHARACTERPARTS.Costume)
        {
            spriteList = IntroSceneManager.Instance.CharacterResourcesManager.CostumeList;
            curNum = ChangeCurNum(ref _curCostumeNum, spriteList.Count, arrowDir);
            ChangeUIImage(_costumeImage, spriteList, curNum);        
        }
    }

    // ����num�� ���⿡ �°� �����ؼ� ��ȯ
    private int ChangeCurNum(ref int curNum, int listCount, ARROWDIR arrowDir)
    {
        if (arrowDir == ARROWDIR.Next)
        {
            curNum++;
            if (curNum >= listCount)
                curNum = 0;
            return curNum;
        }
        else if (arrowDir == ARROWDIR.Prev)
        {
            curNum--;
            if (curNum < 0)
                curNum = listCount - 1;
            return curNum;
        }
        Debug.Log("if�� �Ȱɷ���");
        return 111;
    }

    // ���� UI �̹����� �����ϴ� �Լ�
    private void ChangeUIImage(Image targetUIImage, List<Sprite> spriteList, int currentNum)
    {
        targetUIImage.sprite = spriteList[currentNum];
    }    

    // �Ϸ�� ĳ�������� �ְ� ��ȯ
    private CharacterData CompleteMyCharacterData()
    {
        _completeCharacterData.Name = _nickNameinputField.text;
        _completeCharacterData.Hair = _hairImage.sprite;
        _completeCharacterData.Face = _faceImage.sprite;
        _completeCharacterData.Costume = _costumeImage.sprite;

        return _completeCharacterData;
    }       
}
