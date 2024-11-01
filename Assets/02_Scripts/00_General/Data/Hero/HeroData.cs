using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroID
{
    Warrior,
    Archor, 
    Wizard
}

// ���� ������
[System.Serializable]
public class HeroData : Creature
{
    public float Range;
    public float Delay;
    public string Desc;
    public Sprite Sprite;
}
