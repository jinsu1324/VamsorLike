using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System;

public class ReflectionStudyScene : MonoBehaviour
{
    public TextAsset textAsset;

    private void Awake()
    {
        LoadCSV.CSV_to_MonsterData(textAsset);
        //SaveDataasScriptableObject(2);
    }   
}
