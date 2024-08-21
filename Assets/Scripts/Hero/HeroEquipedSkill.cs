using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ������ �����ϰ��ִ� ��ų�� : �������� ��ų ����Ʈ / ����Ʈ�� ��ų ���ϱ� / ����Ʈ�� ��ų ����
public class HeroEquipedSkill : SerializedMonoBehaviour
{
    #region �̱���
    private static HeroEquipedSkill _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static HeroEquipedSkill Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }

            return _instance;
        }
    }
    #endregion

    // ������ �����ϰ��ִ� ��ų��
    public List<Skill> EquipedSkillList { get; set; } = new List<Skill>();
       

    // ���� ��ų ����Ʈ�� ��ų �߰�
    public void AddSkill(SkillID choicedSkillID)
    {
        Skill choicedSkill = ReturnSkillByID(choicedSkillID);
        EquipedSkillList.Add(choicedSkill);
    }

    // ���� ��ų ����Ʈ�� ��ų ����
    public void RemoveSkill(SkillID choicedSkillID)
    {
        Skill choicedSkill = ReturnSkillByID(choicedSkillID);
        EquipedSkillList.Remove(choicedSkill);
    }

    // SkillID�� ���� Skill�� ��ȯ���ִ� �Լ�
    private Skill ReturnSkillByID(SkillID skillID)
    {
        // ��ų ������ ��ųʸ�
        Dictionary<SkillID, SkillData> skillDataDict = Managers.Instance.DataManager.SkillDataDict;
        // �̹����� �÷������� ����
        HeroObject thisGameHeroObject = PlaySceneManager.ThisGameHeroObject;

        // ��ųID�� ���� Skill �ν��Ͻ�ȭ �ؼ� ��ȯ
        switch (skillID)
        {
            case SkillID.SlashAttack:
                SkillSlashAttack skillSlashAttack = new SkillSlashAttack(skillDataDict[skillID], thisGameHeroObject.transform.position);
                Debug.Log("skillSlashAttack ����!");
                return skillSlashAttack;

            case SkillID.Sniper:
                SkillSniper skillSniper = new SkillSniper(skillDataDict[skillID], thisGameHeroObject.transform.position);
                Debug.Log("skillSniper ����!");
                return skillSniper;

            case SkillID.Boomerang:
                SkillBoomerang skillBoomerang = new SkillBoomerang(skillDataDict[skillID], thisGameHeroObject.transform.position);
                Debug.Log("skillBoomerang ����!");
                return skillBoomerang;

            case SkillID.Heal:
                SkillBoomerang skillHeal = new SkillBoomerang(skillDataDict[skillID], thisGameHeroObject.transform.position);
                Debug.Log("skillHeal ����! (�ӽ÷� �θ޶� ������)");
                return skillHeal;
        }

        Debug.Log("ReturnSkillByID �ƹ��͵� ��ȯ���� ���߽��ϴ�");
        return null;
    }
}
