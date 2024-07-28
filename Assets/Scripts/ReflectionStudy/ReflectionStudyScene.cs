using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ReflectionStudyScene : MonoBehaviour
{

    public TextAsset textAsset;

    private void Awake()
    {
        LoadCSV.CSV_to_MonsterData(textAsset);

        foreach (MonsterData monsterData in MonsterDataManager._monsterDataDict.Values)
        {
            //Debug.Log(monsterData);

            AssetDatabase.CreateAsset(monsterData, $"Assets/Resources/{monsterData.NAME}.asset");

            AssetDatabase.SaveAssets();
        }
     
    }
}
