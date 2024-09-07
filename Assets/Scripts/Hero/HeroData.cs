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

// 영웅 데이터
[System.Serializable]
public class HeroData : Creature
{
    public float Range;
    public float Delay;
    public Sprite Sprite;
}
