using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterID
{
    Bat1,
    Bat2,
    Viper,
    Spider1,
    Spider2,
    Skeleton1,
    Skeleton2,
    OrcAssassin,
    MutantRat
}

// ���� ������
[System.Serializable]
public class MonsterData : BaseData
{
    public string Name;
    public float MaxHp;
    public float Atk;
    public float Speed;
    //public Sprite Sprite;
}
