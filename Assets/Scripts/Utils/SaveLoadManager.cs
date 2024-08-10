using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameData
{
    public string Name;
    public int Score;
    public ItemData ItemData;

    public override string ToString()
    {
        return $"{Name}/{Score}/{ItemData.ItemName}/{ItemData.ItemCount}";
    }
}

[System.Serializable]
public class ItemData
{
    public int ItemCount;
    public string ItemName;
}

public class SaveLoadManager : MonoBehaviour
{
    private string _path;

    private void Start()
    {
        _path = Path.Combine(Application.persistentDataPath, "saveData.json");

        Debug.Log(_path);

        GameData gameData = new GameData()
        {
            Name = "name!!",
            Score = 1000,
            ItemData = new ItemData() { ItemCount = 10, ItemName = "체력포션" }
        };

        //SaveData(gameData);
        GameData gameData2 = LoadData();
        Debug.Log(gameData2);
    }

    public void SaveData(GameData data)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(_path, jsonData);
    }

    public GameData LoadData()
    {
        if (File.Exists(_path))
        {
            string jsonData = File.ReadAllText(_path);
            return JsonUtility.FromJson<GameData>(jsonData);
        }

        return new GameData();
        
    }
}
