using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


/// <summary>
/// Sniper ��ų Ŭ���� : ��Ÿ�� bool �Ǻ� / ���ݷ���
/// </summary>
public class Skill_Sniper : Skill_Base
{
    private SkillData_Sniper _skillData_Sniper = new SkillData_Sniper();
    // ������Ÿ��
    private ProjectileSniper _spawnedProjectileSniper;

    // ������
    public Skill_Sniper() : base("Sniper", "�������� ��", "���Ÿ� ������ �մϴ�.", 3)
    {

    }


    /// <summary>
    /// ��ų ��Ÿ�� ���� (�ð� ������ ���� ��ų ���� �������� true false ��ȯ)
    /// </summary>
    public override bool SkillCooltime()
    {
        _time += Time.fixedDeltaTime;

        if (_time > _skillData_Sniper.Delay)
        {
            _time %= _skillData_Sniper.Delay;
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// ��ų ����
    /// </summary>
    public override void UseSkill(SkillAttackArgs skillAttackArgs)
    {
        //// ��Ÿ� �� ���� ����� ���� ã��
        //MonsterObject closestTargetMonster =
        //    MonsterManager.Instance.GetClosestMonstersByDistance(skillAttackArgs.StartSkillPos, _skillData_Sniper.Range);

        //// ������Ÿ�� ����
        //_spawnedProjectileSniper =
        //    Object.Instantiate(_skillData_Sniper.Projectile, skillAttackArgs.StartSkillPos, Quaternion.identity) as ProjectileSniper;

        //// ���ݷ� �ǳ���
        //_spawnedProjectileSniper.TakeSkillAtk(_skillData_Sniper.Atk);

        //// ������Ÿ�� Ÿ�ٸ��ͷ� �̵�
        //_spawnedProjectileSniper.SettingProjectileInfo(closestTargetMonster);

        Debug.Log("Sniper ��ų ����");
    }
}
