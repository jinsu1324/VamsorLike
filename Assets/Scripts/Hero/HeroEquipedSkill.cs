using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ������ �����ϰ��ִ� ��ų�� : �������� ��ų ����Ʈ / ����Ʈ�� ��ų ���ϱ� / ����Ʈ�� ��ų ����
public class HeroEquipedSkill
{
    // ������ �����ϰ��ִ� ��ų��
    public static List<Skill_Base> EquipedSkillList { get; set; } = new List<Skill_Base>();
       

    // ���� ��ų ����Ʈ�� ��ų �߰�
    public void AddSkill(SkillID choicedSkillID)
    {
        Skill_Base choicedSkill = ReturnSkillByID(choicedSkillID);
        EquipedSkillList.Add(choicedSkill);
    }

    // ���� ��ų ����Ʈ�� ��ų ����
    public void RemoveSkill(SkillID choicedSkillID)
    {
        Skill_Base choicedSkill = ReturnSkillByID(choicedSkillID);
        EquipedSkillList.Remove(choicedSkill);
    }

    // SkillID�� ���� Skill�� ��ȯ���ִ� �Լ�
    private Skill_Base ReturnSkillByID(SkillID skillID)
    {
        HeroObject thisGameHeroObject = PlaySceneManager.ThisGameHeroObject;

        // ��ųID�� ���� Skill �ν��Ͻ�ȭ �ؼ� ��ȯ
        switch (skillID)
        {
            case SkillID.SlashAttack:
                Skill_SlashAttack skillSlashAttack = 
                    new Skill_SlashAttack(
                        DataManager.Instance.SkillData_as_SkillDataDict<SkillData_SlashAttack>(SkillID.SlashAttack, 0), 
                        thisGameHeroObject.transform.position);
                Debug.Log("skillSlashAttack ����!");
                return skillSlashAttack;

            //case SkillID.Sniper:
            //    SkillSniper skillSniper = new SkillSniper(skillDataDict[skillID], thisGameHeroObject.transform.position);
            //    Debug.Log("skillSniper ����!");
            //    return skillSniper;

            //case SkillID.Boomerang:
            //    SkillBoomerang skillBoomerang = new SkillBoomerang(skillDataDict[skillID], thisGameHeroObject.transform.position);
            //    Debug.Log("skillBoomerang ����!");
            //    return skillBoomerang;
        }

        Debug.Log("ReturnSkillByID �ƹ��͵� ��ȯ���� ���߽��ϴ�");
        return null;
    }


    
}
