using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelID
{
    Level0,
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
    Level6,
    Level7,
    Level8,
    Level9,
    Level10,
}

[System.Serializable]
public class LevelData : BaseData
{
    public int Level;
    public int MaxExp;
}
