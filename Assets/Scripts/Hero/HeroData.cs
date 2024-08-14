using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroID
{
    Warrior,
    Archor, 
    Wizard,
    Thief
}

// øµøı µ•¿Ã≈Õ
[System.Serializable]
public class HeroData : ScriptableObject
{
    public string Id;
    public string Name;
    public int Hp;
    public int Atk;
    public float Speed;
    public float Range;
    public float Delay;
    public Sprite Sprite;
}
