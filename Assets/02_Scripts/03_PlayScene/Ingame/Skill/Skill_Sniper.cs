using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Sniper : Skill_Base
{
    private float _skillAtk;                // ��ų ���ݷ�
    private float _projectileSpeed;         // ������Ÿ�� ���ǵ�
    private float _range;                   // ����
    private float _delay;                   // ������
    private ProjectileBase _projectile;     // ��ų ������Ÿ��

    /// <summary>
    /// ������
    /// </summary>
    public Skill_Sniper(SkillData skillData)
    {
        // �⺻ ���� �Ҵ�
        ID = (SkillID)Enum.Parse(typeof(SkillID), skillData.ID);
        CurrentLevel = 1;
        MaxLevel = PlaySceneManager.Instance.SkillManager.GetSkillMaxLevel(ID);

        // ��ų ���� ����
        StatSetting(skillData);
    }

    /// <summary>
    /// ��ų ���� ����
    /// </summary>
    private void StatSetting(SkillData skillData)
    {
        _skillAtk = skillData.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
        _projectileSpeed = skillData.ProjectileSpeed;
        _range = skillData.Range;
        _delay = skillData.Delay;
        _projectile = ResourceManager.Instance.SkillProjectileDict[ID];
    }

    /// <summary>
    /// ��ų ������
    /// </summary>
    public override void LevelUp()
    {
        // ���緹���� 1 ������Ű��, �� ������ ��ų�����͸� �ٽ� ������
        SkillData skillData =
            PlaySceneManager.Instance.SkillManager.GetSkillData_by_SkillIDLevel(ID, ++CurrentLevel);

        // �������� �������� �ٽ� ����
        StatSetting(skillData);
    }

    /// <summary>
    /// ��ų ��Ÿ�� ����
    /// </summary>
    public override bool SkillCooltime()
    {
        _time += Time.fixedDeltaTime;

        if (_time > _delay)
        {
            _time %= _delay;
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
        //Enemy closestTargetMonster =
        //    PlaySceneManager.Instance.EnemyManager.Get_ClosestEnemy_In_Distance(skillAttackArgs.StartSkillPos, _range);

        //// ������Ÿ�� ����
        //ProjectileSniper _spawnedProjectile =
        //    GameObject.Instantiate(_projectile, skillAttackArgs.StartSkillPos, Quaternion.identity) as ProjectileSniper;

        //// ���ݷ� �ǳ���
        //_spawnedProjectile.SetAtk(_skillAtk);

        //// ������Ÿ�� Ÿ�ٸ��ͷ� �̵�
        //_spawnedProjectile.SettingProjectileInfo(closestTargetMonster);
    }
}
