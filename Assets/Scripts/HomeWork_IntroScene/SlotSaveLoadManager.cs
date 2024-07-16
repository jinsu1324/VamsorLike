using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



public class SlotSaveLoadManager : MonoBehaviour
{
    // public static string _path;

    private void Start()
    {
        //_path = Path.Combine(Application.persistentDataPath, "saveData.json");
        //Debug.Log(_path);
    }

    // 세이브
    public static void SaveData(CharacterData data, SlotNum slotNum, string path)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, jsonData);
    }

    // 로드
    public static CharacterData LoadData(string path)
    {
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            return JsonUtility.FromJson<CharacterData>(jsonData);
        }

        return new CharacterData();

    }
}
