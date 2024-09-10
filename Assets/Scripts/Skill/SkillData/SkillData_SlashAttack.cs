using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData_SlashAttack : SkillDataBase
{
    public string Name;
    public int Level;
    public int Atk;
    public float Delay;
    public string Desc;
    public ProjectileBase Projectile;
}
