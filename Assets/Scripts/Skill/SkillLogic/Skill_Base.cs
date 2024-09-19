using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public struct SkillAttackArgs
{
    public Vector3 StartSkillPos;   
}


/// <summary>
/// ��� ��ų�� �θ� Ŭ����
/// </summary>
public abstract class Skill_Base
{
    public string Id;
    public string Name;
    public string Desc;
    public int CurrentLevel;
    public int MaxLevel;

    protected float _time;    

    /// <summary>
    /// ��ų ������ �Լ�
    /// </summary>
    public bool LevelUp()
    {
        if (CurrentLevel < MaxLevel)
        {
            CurrentLevel++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public abstract bool SkillCooltime();

    public abstract void UseSkill(SkillAttackArgs skillAttackArgs);

}



