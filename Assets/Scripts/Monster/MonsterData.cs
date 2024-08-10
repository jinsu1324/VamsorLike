using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ∏ÛΩ∫≈Õ ID
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
    public string Name;
    public int Hp;
    public int Atk;
    public float Speed;
}
