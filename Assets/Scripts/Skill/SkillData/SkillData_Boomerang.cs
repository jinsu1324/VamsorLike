using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData_Boomerang : SkillData_Base
{
    public int Level;
    public float AtkPercentage;
    public float ProjectileSpeed;
    public int ProjectileCount;
    public float Range;
    public string Desc;
    public ProjectileBase Projectile;
    public Sprite Icon;
}
