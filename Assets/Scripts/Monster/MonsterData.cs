using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterID
{
    Golem,
    Skeleton,
    Witch,
    Dragon
}

[System.Serializable]
public class MonsterData : ScriptableObject
{
    public string Id;
    public string Name;
    public int Hp;
    public int Atk;
    public float Speed;
    public Sprite Sprite;
}
