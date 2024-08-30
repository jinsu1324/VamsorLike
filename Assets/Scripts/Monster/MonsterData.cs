using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MONSTERID
{
    Golem,
    Skeleton,
    Witch,
    Dragon
}

// 몬스터 데이터
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
