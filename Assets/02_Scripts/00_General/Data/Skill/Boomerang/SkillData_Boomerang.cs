using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillData_BoomerangID
{
    Level0,
    Level1,
    Level2
}

[System.Serializable]
public class SkillData_Boomerang : BaseData
{
    public int Level;
    public string Name;
    public string Desc;
    public float AtkPercentage;
    public float ProjectileSpeed;
    public int ProjectileCount;
    public float Range;
    public ProjectileBase Projectile;
    public Sprite Icon;
}
