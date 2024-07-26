using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionStudyScene : MonoBehaviour
{

    public TextAsset textAsset;

    private void Awake()
    {
        LoadCSV.CSV_to_Data(textAsset);

        foreach (MonsterInfo monsterInfo in MonsterInfo.monsterInfos.Values)
        {
            Debug.Log(monsterInfo);
        }
    }
}
