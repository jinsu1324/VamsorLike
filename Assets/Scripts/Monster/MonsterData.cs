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
    public string NAME;
    public int HP;
    public int ATK;
    public float SPEED;
}
