using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData_SlashAttack : SkillData_Base
{
    public int Level;
    public int Atk;
    public float Delay;
    public string Desc;
    public ProjectileBase Projectile;
    public Sprite Icon;
}
