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
                SkillSlashAttack skillBoomerang = new SkillSlashAttack(skillDataDict[skillID], thisGameHeroObject.transform.position);
                Debug.Log("SlashAttack �� ���õǾ����ϴ�!");
                return skillBoomerang;

            case SkillID.Sniper:
                SkillBoomerang skillBoomerang2 = new SkillBoomerang(skillDataDict[skillID], thisGameHeroObject.transform.position);
                Debug.Log("Sniper �� ���õǾ����ϴ�!(�ӽ÷� �θ޶� ������)");
                return skillBoomerang2;

            case SkillID.Boomerang:
                SkillBoomerang skillBoomerang3 = new SkillBoomerang(skillDataDict[skillID], thisGameHeroObject.transform.position);
                Debug.Log("Boomerang �� ���õǾ����ϴ�!");
                return skillBoomerang3;

            case SkillID.Heal:
                SkillBoomerang skillBoomerang4 = new SkillBoomerang(skillDataDict[skillID], thisGameHeroObject.transform.position);
                Debug.Log("Heal �� ���õǾ����ϴ�!(�ӽ÷� �θ޶� ������)");
                return skillBoomerang4;
        }

        Debug.Log("ReturnSkillByID �ƹ��͵� ��ȯ���� ���߽��ϴ�");
        return null;
    }
}
