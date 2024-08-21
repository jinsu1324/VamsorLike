using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

// �� ��ų�� � ��ų���� ������ �ִ� ����
public class SkillSniper : Skill
{
    // ������Ÿ��
    private ProjectileSniper _spawnedProjectileSniper;

    // ������
    public SkillSniper(SkillData skillData, Vector3 pos)
    {
        // SkillSniper�� �ν��Ͻ�ȭ �Ǵ� ����, �Ű������� �޾ƿ� �����Ͱ��� �� SkillSlashAttack�� �����ͷ� �־���
        _skillData = skillData;
    }

    public override bool SkillUpdate()
    {
        _time += Time.fixedDeltaTime;

        if (_time > _skillData.Delay)
        {
            _time %= _skillData.Delay;
            return true;
        }
        else
        {
            return false;
        }
    }

    // ���� ����
    public override void AttackFunc(Vector3 skillPos)
    {
        // ��Ÿ� �� ������ ���� ã��
        //List<MonsterObject> closeMonsterList = new List<MonsterObject>();
        //closeMonsterList = SceneSingleton<MonsterManager>.Instance.GetMonstersByDistance(skillPos, _skillData.Range);

        //MonsterObject targetMonster = closeMonsterList[Random.Range(0, closeMonsterList.Count)];


        // ��Ÿ� �� ���� ����� ���� ã��
        MonsterObject closestTargetMonster = 
            SceneSingleton<MonsterManager>.Instance.GetClosestMonstersByDistance(skillPos, _skillData.Range);

        // ������Ÿ�� ����
        _spawnedProjectileSniper =
            Object.Instantiate(_skillData.Projectile, skillPos, Quaternion.identity) as ProjectileSniper;

        // ���ݷ� �ǳ���
        _spawnedProjectileSniper.TakeSkillAtk(_skillData.Atk);

        // ������Ÿ�� Ÿ�ٸ��ͷ� �̵�
        _spawnedProjectileSniper.SettingProjectileInfo(closestTargetMonster);                


        //for (int i = 0; i < closeMonsterList.Count; i++)
        //{
        //    closeMonsterList[i].HPMinus(_heroSkillStatus.ATK);
        //    break;
        //}
    }
}
