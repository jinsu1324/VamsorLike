using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum NextPrev
{
    Next,
    Prev
}

public class CharacterMakePopup : MonoBehaviour
{
    
    public TextMeshProUGUI _titleText;
   
    public TMP_InputField _inputField;

    public Action<string, Sprite, Sprite, Sprite> CompleteButtonAction;

    public Action ExitButtonAction;


    [SerializeField]
    private Image _hairImage;
    [SerializeField]
    private Image _faceImage;
    [SerializeField]
    private Image _costumeImage;

    [SerializeField]
    private int _hairNum;
    [SerializeField]
    private int _faceNum;
    [SerializeField]
    private int _costumeNum;

    private void OnEnable()
    {
        CharacterImageInit();
    }

    private void CharacterImageInit()
    {
        _hairNum = 0;
        _faceNum = 0;
        _costumeNum = 0;

        _hairImage.sprite = CharacterResourcesManager.Instance.HairList[_hairNum];
        _faceImage.sprite = CharacterResourcesManager.Instance.FaceList[_faceNum];
        _costumeImage.sprite = CharacterResourcesManager.Instance.CostumeList[_costumeNum];
    }

    public void NextHairButton()
    {
        _hairNum++;
        _hairImage.sprite = 
            CharacterResourcesManager.Instance.HairList[
                _hairNum % CharacterResourcesManager.Instance.HairList.Count];
    }

    public void NextFaceButton()
    {
        _faceNum++;
        _faceImage.sprite = 
            CharacterResourcesManager.Instance.FaceList[
                _faceNum % CharacterResourcesManager.Instance.FaceList.Count];
    }

    public void NextCostumeButton()
    {
        _costumeNum++;
        _costumeImage.sprite = 
            CharacterResourcesManager.Instance.CostumeList[
                _costumeNum % CharacterResourcesManager.Instance.CostumeList.Count];
    }


    public void OnClickCompleteButton()
    {
        if (_inputField.text == "")
        {
            Debug.Log("닉네임을 입력하세요!");
        }
        else
        {
            CompleteButtonAction(_inputField.text, _hairImage.sprite, _faceImage.sprite, _costumeImage.sprite);
        }  
    }

    public void OnClickExitButton()
    {
        ExitButtonAction();
    }

}
