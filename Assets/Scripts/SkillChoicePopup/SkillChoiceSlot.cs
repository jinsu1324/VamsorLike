using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// ��ų ���ý���
public class SkillChoiceSlot : SerializedMonoBehaviour, IPointerClickHandler
{
    // �� ���Կ� ���� ��ų �̸�
    [SerializeField]
    public TextMeshProUGUI SkillNameText { get; set; }

    // �� ���Կ� ���� ��ų ID
    public SkillID SkillID { get; set; }

    // ���� �Ϸ����� �� �׼�
    private Action _onSelectFinish;


    // ���� Ŭ������ ��
    public void OnPointerClick(PointerEventData eventData)
    {
        // ���� ���� ��ų����Ʈ�� �߰�
        AddEquipedSkillList(SkillID);

        // ��ų �����˾� �ݱ�
        _onSelectFinish();

        // ���ӽ��� �ȵǾ��־��ٸ�
        if (PlaySceneManager.Instance.IsGameStart == false)
        {
            // ���ӽ����� true��
            PlaySceneManager.Instance.IsGameStartChange(true);

            // ���� ���� ����
            MonsterSpawner.Instance.StartMonsterSpawn();            
        }
    }

    // ���� ������ ����
    public void SetSlotInfos(SkillID skillID, Action popupClose)
    {
        SkillID = skillID;      

        switch (skillID)
        {
            case SkillID.SlashAttack:
                SkillData_SlashAttack slashAttack = DataManager.Instance.SkillDataDict[SkillID.SlashAttack].SkillDataList[0] as SkillData_SlashAttack;
                SkillNameText.text = slashAttack.Name;
                break;
            case SkillID.Sniper:
                SkillData_Sniper sniper = DataManager.Instance.SkillDataDict[SkillID.Sniper].SkillDataList[0] as SkillData_Sniper;
                SkillNameText.text = sniper.Name;
                break;
            case SkillID.Boomerang:
                SkillData_Boomerang boomerang = DataManager.Instance.SkillDataDict[SkillID.Boomerang].SkillDataList[0] as SkillData_Boomerang;
                SkillNameText.text = boomerang.Name;
                break;
            default:
                break;
        }
        
        _onSelectFinish += popupClose;
    }

    // ������ ��ų����Ʈ�� �߰�
    private void AddEquipedSkillList(SkillID skillID)
    {
        HeroEquipedSkill heroEquipedSKill = new HeroEquipedSkill();
        heroEquipedSKill.AddSkill(skillID);
    }

}
