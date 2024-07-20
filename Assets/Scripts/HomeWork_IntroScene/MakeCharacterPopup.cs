using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MakeCharacterPopup : MonoBehaviour
{
    
    public TextMeshProUGUI _titleText;
   
    public TMP_InputField _inputField;
   
    public Button _completeButton;

    public Action<string> CompleteButtonAction;

    public void OnClickCompleteButton()
    {
        CompleteButtonAction(_inputField.text);
    }

}
