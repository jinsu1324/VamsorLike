using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMakePopup : MonoBehaviour
{
    
    public TextMeshProUGUI _titleText;
   
    public TMP_InputField _inputField;

    public Action<string> CompleteButtonAction;

    public Action ExitButtonAction;


    public void OnClickCompleteButton()
    {
        if (_inputField.text == "")
        {
            Debug.Log("닉네임을 입력하세요!");
        }
        else
        {
            CompleteButtonAction(_inputField.text);
        }  
    }

    public void OnClickExitButton()
    {
        ExitButtonAction();
    }

}
