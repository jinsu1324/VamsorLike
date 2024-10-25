using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    public TimeSpan Wave;
    public string[] MonsterType;
    public int[] Quantity;
    public float SpawnInterval;
    public string BossType;
    public string BossSpawnTime;
    public string AdditionalNotes;
}
