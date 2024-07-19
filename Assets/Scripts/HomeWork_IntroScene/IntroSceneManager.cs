using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    private void Awake()
    {
        CharacterDataManager.LoadCharacterData();
    }

}
