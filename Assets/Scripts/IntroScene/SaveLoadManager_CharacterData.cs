using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



public class SaveLoadManager_CharacterData : MonoBehaviour
{
    // ���̺�
    public static void SaveData(CharacterData data, string path)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, jsonData);
    }

    // �ε�
    public static CharacterData LoadData(string path)
    {
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            return JsonUtility.FromJson<CharacterData>(jsonData);
        }

        return null;

    }
}
