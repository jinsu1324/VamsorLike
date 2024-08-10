using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{   
    [SerializeField]
    private CharacterMakePopup _characterMakePopup;
    public CharacterMakePopup CharacterMakePopup { get { return _characterMakePopup; } }

    [SerializeField]
    private TextAsset _textAsset;

    private void Awake()
    {
        CharacterDataManager.LoadCharacterData();
    }

}
