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
        //SkillID = skillID;
        //SkillNameText.text = DataManager.Instance.SkillDataDict[skillID].Name;

        //_onSelectFinish += popupClose;
    }

    // ������ ��ų����Ʈ�� �߰�
    private void AddEquipedSkillList(SkillID skillID)
    {
        HeroEquipedSkill heroEquipedSKill = new HeroEquipedSkill();
        heroEquipedSKill.AddSkill(skillID);
    }

}
