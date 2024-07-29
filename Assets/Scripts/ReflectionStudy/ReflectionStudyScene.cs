using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class ReflectionStudyScene : MonoBehaviour
{

    public TextAsset textAsset;

    private void Awake()
    {
        LoadCSV.CSV_to_MonsterData(textAsset);
        SaveDataasScriptableObject();
    }

    private void SaveDataasScriptableObject()
    {
        foreach (MonsterData monsterData in MonsterDataManager._monsterDataDict.Values)
        {
            if (File.Exists($"Assets/Resources/{monsterData.NAME}.asset"))
                continue;

            AssetDatabase.CreateAsset(monsterData, $"Assets/Resources/{monsterData.NAME}.asset");
            AssetDatabase.SaveAssets();
        }
    }
}
