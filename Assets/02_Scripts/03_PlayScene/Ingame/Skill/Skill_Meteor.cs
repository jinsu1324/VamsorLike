using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill_Meteor : Skill_Base
{
    private ProjectileMeteorArgs _statArgs = new ProjectileMeteorArgs();    // ������Ÿ�� ���׿��� �ʿ��� ���ȵ�
    private int _projectileCount;                                           // ������Ÿ�� ����
    private float _delay;                                                   // ������
    private float _range;                                                   // ����
    private ProjectileMeteor _projectile;                                   // ������Ÿ��

    /// <summary>
    /// ������
    /// </summary>
    public Skill_Meteor(SkillData skillData)
    {
        // �⺻ ���� �Ҵ�
        ID = (SkillID)Enum.Parse(typeof(SkillID), skillData.ID);
        CurrentLevel = 1;
        MaxLevel = PlaySceneManager.Instance.SkillManager.GetSkillMaxLevel(ID);
        _projectile = ResourceManager.Instance.SkillProjectileDict[ID] as ProjectileMeteor;

        // ���� ����
        StatSetting(skillData);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void StatSetting(SkillData skillData)
    {
        _statArgs.Atk = skillData.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
        _delay = skillData.Delay;
        _range = skillData.Range;
        _projectileCount = skillData.ProjectileCount;
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
        PlaySceneManager.Instance.SkillManager.StartCoroutine(MeteorSkill(skillAttackArgs));
    }

    /// <summary>
    /// ���׿� ��ų �ڷ�ƾ
    /// </summary>
    private IEnumerator MeteorSkill(SkillAttackArgs skillAttackArgs)
    {
        List<Enemy> targetEnemyList = PlaySceneManager.Instance.EnemyManager.Get_RandomEnemys_In_Distance(
            skillAttackArgs.StartSkillPos, _range, _projectileCount);
        
        if (targetEnemyList.Count == 0)
            yield break;

        for (int i = 0; i < targetEnemyList.Count; i++)
        {
            // ������Ÿ�� ����
            ProjectileMeteor projectile = GameObject.Instantiate(
                _projectile, targetEnemyList[i].transform.position, Quaternion.identity);

            // ������ ������Ÿ�� ���� ����
            projectile.SetStats(_statArgs);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
