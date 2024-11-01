using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroID
{
    Warrior,
    Archor, 
    Wizard
}

[System.Serializable]
public class HeroData : BaseData
{
    public string Name;
    public float MaxHp;
    public float Atk;
    public float Speed;
    public string StartSkill;
    public string Desc;
}
