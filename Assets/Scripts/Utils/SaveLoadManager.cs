using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager
{
    // ���̺�
    public static void SaveData<DataType>(DataType data, string path)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, jsonData);
    }

    // �ε�
    public static DataType LoadData<DataType>(string path) where DataType : class
    {
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            return JsonUtility.FromJson<DataType>(jsonData);
        }

        return null;
    }
}
