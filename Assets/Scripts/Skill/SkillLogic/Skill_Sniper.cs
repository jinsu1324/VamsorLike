using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

// �� ��ų�� � ��ų���� ������ �ִ� ����
public class Skill_Sniper : Skill_Base
{
    // ������Ÿ��
    private ProjectileSniper _spawnedProjectileSniper;

    // ������
    public Skill_Sniper(SkillData_Base skillData, Vector3 pos)
    {
        // SkillSniper�� �ν��Ͻ�ȭ �Ǵ� ����, �Ű������� �޾ƿ� �����Ͱ��� �� SkillSlashAttack�� �����ͷ� �־���
        //_skillData = skillData;
    }

    public override bool SkillUpdate()
    {
        //_time += Time.fixedDeltaTime;

        //if (_time > _skillData.Delay)
        //{
        //    _time %= _skillData.Delay;
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}

        return false;
    }

    // ���� ����
    public override void AttackFunc(SkillAttackArgs skillAttackArgs)
    {
        //// ��Ÿ� �� ���� ����� ���� ã��
        //MonsterObject closestTargetMonster = 
        //    MonsterManager.Instance.GetClosestMonstersByDistance(skillAttackArgs.StartSkillPos, _skillData.Range);

        //// ������Ÿ�� ����
        //_spawnedProjectileSniper =
        //    Object.Instantiate(_skillData.Projectile, skillAttackArgs.StartSkillPos, Quaternion.identity) as ProjectileSniper;

        //// ���ݷ� �ǳ���
        //_spawnedProjectileSniper.TakeSkillAtk(_skillData.Atk);

        //// ������Ÿ�� Ÿ�ٸ��ͷ� �̵�
        //_spawnedProjectileSniper.SettingProjectileInfo(closestTargetMonster);                


    
    }
}
