using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameData
{
    public string _name;
    public int _score;
    public ItemData _itemData;

    public override string ToString()
    {
        return $"{_name}/{_score}/{_itemData._itemName}/{_itemData._itemCount}";
    }
}

[System.Serializable]
public class ItemData
{
    public int _itemCount;
    public string _itemName;
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
            _name = "name!!",
            _score = 1000,
            _itemData = new ItemData() { _itemCount = 10, _itemName = "체력포션" }
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
