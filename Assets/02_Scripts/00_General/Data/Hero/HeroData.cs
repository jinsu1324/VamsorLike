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
    public string StartSkill;
    public string Desc;
}
