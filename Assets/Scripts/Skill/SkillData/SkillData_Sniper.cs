using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData_Sniper : SkillData_Base
{
    public int Level;
    public float AtkPercentage;
    public float ProjectileSpeed;
    public float Range;
    public float Delay;
    public string Desc;
    public ProjectileBase Projectile;
    public Sprite Icon;
}
