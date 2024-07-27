using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ReflectionStudyScene : MonoBehaviour
{

    public TextAsset textAsset;

    private void Awake()
    {
        LoadCSV.CSV_to_Data(textAsset);

        foreach (MonsterInfo monsterInfo in MonsterDataManager._monsterInfoDict.Values)
        {
            Debug.Log(monsterInfo);
        }

        Debug.Log(MonsterDataManager._monsterInfoDict[MonsterKey.Golem.ToString()].HP);






        //MonsterInfo monsterInfoScriptable = ScriptableObject.CreateInstance<MonsterInfo>();

        //monsterInfoScriptable.NAME = "Goblin";
        //monsterInfoScriptable.HP = 100;
        //monsterInfoScriptable.ATK = 1000;
        //monsterInfoScriptable.SPEED = 12.5f;

        //AssetDatabase.CreateAsset(monsterInfoScriptable, $"Assets/Resources/{monsterInfoScriptable.NAME}.asset");

        //AssetDatabase.SaveAssets();
    }
}
