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
    // UI 이미지들
    [Title("UI Images", bold: false)]
    [SerializeField]
    private Image _hairImage;
    [SerializeField]
    private Image _faceImage;
    [SerializeField]
    private Image _costumeImage;

    // 닉네임 인풋필드
    [Title("")]
    [SerializeField]
    private TMP_InputField _nickNameinputField;

    // 어떤슬롯을 선택해서 뜬 팝업인지
    private SLOTNUM _selectedSlotNum;

    // 현재 선택되어있는 파츠 넘버
    private int _curHairNum;
    private int _curFaceNum;
    private int _curCostumeNum;

    // 완료된 캐릭터 데이터
    private CharacterData _completeCharacterData = new CharacterData();


    // 팝업 열릴때 호출
    public void OpenPopup(SLOTNUM slotNum)
    {        
        _selectedSlotNum = slotNum;

        AllCurNumInit();
        AllUIImageInit();

        this.gameObject.SetActive(true);
    }    

    // Next버튼 누르면 호출
    public void OnClickNextButton(int characterPartsNum)
    {
        // 다음 이미지로 변경
        ChangeSprite((CHARACTERPARTS)characterPartsNum, ARROWDIR.Next);
    }

    // Prev버튼 누르면 호출
    public void OnClickPrevButton(int characterPartsNum)
    {
        // 이전 이미지로 변경
        ChangeSprite((CHARACTERPARTS)characterPartsNum, ARROWDIR.Prev);
    }

    // Complete버튼 누르면 호출
    public void OnClickCompleteButton()
    {
        if (_nickNameinputField.text == "")
        {
            Debug.Log("닉네임을 입력하세요!");
        }
        else
        {
            IntroSceneManager.Instance.CharacterDataManager.MakeNewCharacterData(_selectedSlotNum, CompleteMyCharacterData()); 
            IntroSceneManager.Instance.CharacterSlotManager.InitSlots();
            OnClickExitButton();
        }
    }

    // Exit버튼 누르면 호출
    public void OnClickExitButton()
    {
        gameObject.SetActive(false);
    }

    // 모든 이미지Num 0으로 초기화
    private void AllCurNumInit()
    {
        _curHairNum = 0;
        _curFaceNum = 0;
        _curCostumeNum = 0;
    }

    // 모든 UI이미지 0번째 이미지로 초기화
    private void AllUIImageInit()
    {
        _hairImage.sprite = IntroSceneManager.Instance.CharacterResourcesManager.HairList[_curHairNum];
        _faceImage.sprite = IntroSceneManager.Instance.CharacterResourcesManager.FaceList[_curFaceNum];
        _costumeImage.sprite = IntroSceneManager.Instance.CharacterResourcesManager.CostumeList[_curCostumeNum];
    }

    // 캐릭터의 스프라이트 변경
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

    // 현재num을 방향에 맞게 변경해서 반환
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
        Debug.Log("if에 안걸러짐");
        return 111;
    }

    // 실제 UI 이미지를 변경하는 함수
    private void ChangeUIImage(Image targetUIImage, List<Sprite> spriteList, int currentNum)
    {
        targetUIImage.sprite = spriteList[currentNum];
    }    

    // 완료된 캐릭터정보 넣고 반환
    private CharacterData CompleteMyCharacterData()
    {
        _completeCharacterData.Name = _nickNameinputField.text;
        _completeCharacterData.Hair = _hairImage.sprite;
        _completeCharacterData.Face = _faceImage.sprite;
        _completeCharacterData.Costume = _costumeImage.sprite;

        return _completeCharacterData;
    }       
}
