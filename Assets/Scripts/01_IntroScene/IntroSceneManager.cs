using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset _textAsset;

    [SerializeField]
    private CharacterMakePopup _characterMakePopup;
    public CharacterMakePopup CharacterMakePopup { get { return _characterMakePopup; } }



    private void Awake()
    {
        CharacterDataManager.LoadCharacterData();
    }

}
