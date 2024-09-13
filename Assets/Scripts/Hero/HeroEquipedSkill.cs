using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ������ �����ϰ��ִ� ��ų�� : �������� ��ų ����Ʈ / ����Ʈ�� ��ų ���ϱ� / ����Ʈ�� ��ų ����
public class HeroEquipedSkill
{
    // ������ �����ϰ��ִ� ��ų��
    public static List<SkillBase> EquipedSkillList { get; set; } = new List<SkillBase>();
       

    // ���� ��ų ����Ʈ�� ��ų �߰�
    public void AddSkill(SkillID choicedSkillID)
    {
        SkillBase choicedSkill = ReturnSkillByID(choicedSkillID);
        EquipedSkillList.Add(choicedSkill);
    }

    // ���� ��ų ����Ʈ�� ��ų ����
    public void RemoveSkill(SkillID choicedSkillID)
    {
        SkillBase choicedSkill = ReturnSkillByID(choicedSkillID);
        EquipedSkillList.Remove(choicedSkill);
    }

    // SkillID�� ���� Skill�� ��ȯ���ִ� �Լ�
    private SkillBase ReturnSkillByID(SkillID skillID)
    {
        // ��ų ������ ��ųʸ�
        //Dictionary<SkillID, SkillDataBase> skillDataDict = DataManager.Instance.SkillDataDict;
        //// �̹����� �÷������� ����
        //HeroObject thisGameHeroObject = PlaySceneManager.ThisGameHeroObject;

        //// ��ųID�� ���� Skill �ν��Ͻ�ȭ �ؼ� ��ȯ
        //switch (skillID)
        //{
        //    case SkillID.SlashAttack:
        //        SkillSlashAttack skillSlashAttack = new SkillSlashAttack(skillDataDict[skillID], thisGameHeroObject.transform.position);
        //        Debug.Log("skillSlashAttack ����!");
        //        return skillSlashAttack;

        //    case SkillID.Sniper:
        //        SkillSniper skillSniper = new SkillSniper(skillDataDict[skillID], thisGameHeroObject.transform.position);
        //        Debug.Log("skillSniper ����!");
        //        return skillSniper;

        //    case SkillID.Boomerang:
        //        SkillBoomerang skillBoomerang = new SkillBoomerang(skillDataDict[skillID], thisGameHeroObject.transform.position);
        //        Debug.Log("skillBoomerang ����!");
        //        return skillBoomerang;
        //}

        //Debug.Log("ReturnSkillByID �ƹ��͵� ��ȯ���� ���߽��ϴ�");
        return null;
    }
}
