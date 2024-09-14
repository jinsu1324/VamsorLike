using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData_Sniper : SkillDataBase
{
    public int Level;
    public int Atk;
    public float ProjectileSpeed;
    public float Range;
    public float Delay;
    public string Desc;
    public ProjectileBase Projectile;
}
