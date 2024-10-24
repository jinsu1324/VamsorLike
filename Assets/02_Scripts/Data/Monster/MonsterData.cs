using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterID
{
    Golem,
    Skeleton,
    Witch,
    Dragon,
}

// 몬스터 데이터
[System.Serializable]
public class MonsterData : Creature
{
    public Sprite Sprite;
}
