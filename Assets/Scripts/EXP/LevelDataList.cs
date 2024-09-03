using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelDataList : ScriptableObject
{
    public List<LevelData> LevelDatas = new List<LevelData>();

    public void Clear()
    {
        throw new NotImplementedException();
    }
}
