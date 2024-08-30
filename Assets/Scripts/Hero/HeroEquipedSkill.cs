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
    public void AddSkill(SKILLID choicedSkillID)
    {
        SkillBase choicedSkill = ReturnSkillByID(choicedSkillID);
        EquipedSkillList.Add(choicedSkill);
    }

    // ���� ��ų ����Ʈ�� ��ų ����
    public void RemoveSkill(SKILLID choicedSkillID)
    {
        SkillBase choicedSkill = ReturnSkillByID(choicedSkillID);
        EquipedSkillList.Remove(choicedSkill);
    }

    // SkillID�� ���� Skill�� ��ȯ���ִ� �Լ�
    private SkillBase ReturnSkillByID(SKILLID skillID)
    {
        // ��ų ������ ��ųʸ�
        Dictionary<SKILLID, SkillData> skillDataDict = DataManager.Instance.SkillDataDict;
        // �̹����� �÷������� ����
        HeroObject thisGameHeroObject = PlaySceneManager.ThisGameHeroObject;

        // ��ųID�� ���� Skill �ν��Ͻ�ȭ �ؼ� ��ȯ
        switch (skillID)
        {
            case SKILLID.SlashAttack:
                SkillSlashAttack skillSlashAttack = new SkillSlashAttack(skillDataDict[skillID], thisGameHeroObject.transform.position);
                Debug.Log("skillSlashAttack ����!");
                return skillSlashAttack;

            case SKILLID.Sniper:
                SkillSniper skillSniper = new SkillSniper(skillDataDict[skillID], thisGameHeroObject.transform.position);
                Debug.Log("skillSniper ����!");
                return skillSniper;

            case SKILLID.Boomerang:
                SkillBoomerang skillBoomerang = new SkillBoomerang(skillDataDict[skillID], thisGameHeroObject.transform.position);
                Debug.Log("skillBoomerang ����!");
                return skillBoomerang;

            case SKILLID.Heal:
                SkillBoomerang skillHeal = new SkillBoomerang(skillDataDict[skillID], thisGameHeroObject.transform.position);
                Debug.Log("skillHeal ����! (�ӽ÷� �θ޶� ������)");
                return skillHeal;
        }

        Debug.Log("ReturnSkillByID �ƹ��͵� ��ȯ���� ���߽��ϴ�");
        return null;
    }
}
