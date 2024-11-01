using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillData_SniperID
{
    Level0,
    Level1,
    Level2
}

[System.Serializable]
public class SkillData_Sniper : BaseData
{
    public int Level;
    public string Name;
    public string Desc;
    public float AtkPercentage;
    public float ProjectileSpeed;
    public float Range;
    public float Delay;
    public ProjectileBase Projectile;
    public Sprite Icon;
}