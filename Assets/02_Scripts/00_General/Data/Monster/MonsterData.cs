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

// ���� ������
[System.Serializable]
public class MonsterData : Creature
{
    public Sprite Sprite;
}
