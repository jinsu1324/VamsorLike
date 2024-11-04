using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData : BaseData
{
    public int Level;
    public string Name;
    public float AtkPercentage;
    public string Desc;
    public float Delay;                    
    public float Range;                    
    public float ProjectileSpeed;          
    public int ProjectileCount;            
    public ProjectileBase Projectile;
    public Sprite Icon;
}
