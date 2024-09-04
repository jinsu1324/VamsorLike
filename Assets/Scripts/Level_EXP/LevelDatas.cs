using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelDatas : ScriptableObject
{
    public List<LevelData> LevelDataList = new List<LevelData>();

    public void Clear()
    {
        throw new NotImplementedException();
    }
}
