using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveID
{
    Wave1,
    Wave2,
    Wave3,
    Wave4,
    Wave5,
}

[System.Serializable]
public class WaveData : BaseData
{
    public string WaveTime;
    public string[] MonsterType;
    public int[] TotalSpawnCount;
    public float[] SpawnInterval;
    public string BossType;
}
