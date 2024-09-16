using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

// 스킬 부메랑 공격로직, 쿨타임
public class Skill_Boomerang : Skill_Base
{
    // 부메랑 시작해도 되는지
    private bool _isBoomerangStarted = false;

    // 프로젝타일
    private ProjectileBoomerang _SpawnedProjectileBoomerang;

    // 생성자
    public Skill_Boomerang(SkillData_Base skillData, Vector3 pos)
    {
        // 스킬 부메랑이 인스턴스화 되는 순간, 매개변수로 받아온 데이터값을 이 SkillBoomerang의 데이터로 넣어줌
        //_skillData = skillData;

        // 프로젝타일 생성
        // Imsi _SpawnedProjectileBoomerang = 
        // Imsi Object.Instantiate(skillData.Projectile, pos, Quaternion.identity) as ProjectileBoomerang;

        // 부메랑은 생성되면 계속 공격하게끔 만들 예정
        //_isBoomerangStarted = true;
    }

    // 스킬 공격 가능한지 true false 반환
    public override bool SkillUpdate()
    {
        // 부메랑은 생성되면 계속 돌아야한다.
        if (_isBoomerangStarted)
            return true;
        else
            return false;
    }

    // 공격 로직
    public override void AttackFunc(SkillAttackArgs skillAttackArgs)
    {
        //_SpawnedProjectileBoomerang.AroundBoomerang(skillAttackArgs.StartSkillPos);
        //_SpawnedProjectileBoomerang.TakeSkillAtk(_skillData.Atk);
    }    
}
