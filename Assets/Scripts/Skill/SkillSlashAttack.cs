using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class SkillSlashAttack : Skill
{
    // ������Ÿ��
    private ProjectileSlashAttack _spawnedProjectileSlashAttack;

    // ������
    public SkillSlashAttack(SkillData skillData, Vector3 pos)
    {
        // ��ų �����þ����� �ν��Ͻ�ȭ �Ǵ� ����, �Ű������� �޾ƿ� �����Ͱ��� �� SkillSlashAttack�� �����ͷ� �־���
        _skillData = skillData;    
    }

    // ��ų ��Ÿ�� ���� (��ų ���� �������� true false ��ȯ)
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
        // ������Ÿ�� ����
        _spawnedProjectileSlashAttack =
            Object.Instantiate(_skillData.Projectile, skillPos, Quaternion.identity) as ProjectileSlashAttack;

        // ������Ÿ�Ͽ� ���ݷ� �ǳ���
        _spawnedProjectileSlashAttack.TakeSkillAtk(_skillData.Atk);


        //List<MonsterObject> closeMonsterList = new List<MonsterObject>();
        //closeMonsterList = SceneSingleton<MonsterManager>.Instance.GetMonstersByLength(skillPos, _skillData.Range);

        //for (int i = 0; i < closeMonsterList.Count; i++)
        //{
        //    closeMonsterList[i].HPMinus(_skillData.Atk);
        //}

        //Debug.Log("attack");
    }
}
