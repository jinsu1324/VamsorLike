using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ų���ý��Ե� �ִ� �˾� : ��ų���� �迭 / ��ų���ý��Կ� �����ϰ� ��ų �־���
public class SkillChoicePopup : MonoBehaviour
{
    // �����ϴ� ��ų ���Ե�
    [SerializeField]
    private SkillChoiceSlot[] _skillChoiceSlotArr;

    private void Start()
    {
        SettingSkillChoiceSlot();
    }

    // ��ų���Ե� �ʱ�ȭ
    private void SettingSkillChoiceSlot()
    {
        // ��ų �׽�Ʈ�� �ӽ�
        _skillChoiceSlotArr[0].SetSlotInfos(SkillID.SlashAttack, ClosePopup);
        _skillChoiceSlotArr[1].SetSlotInfos(SkillID.Boomerang, ClosePopup);
        _skillChoiceSlotArr[2].SetSlotInfos(SkillID.Sniper, ClosePopup);
        _skillChoiceSlotArr[3].SetSlotInfos(SkillID.Heal, ClosePopup);


        // �������� ���ֱ�

        //for (int i = 0; i < _skillChoiceSlotArr.Length; i++)
        //{
        //    // SkillID�� �������� �ϳ��� �̾ƿ�
        //    SkillID[] skillIDs = System.Enum.GetValues(typeof(SkillID)) as SkillID[];
        //    SkillID skillID = (SkillID)skillIDs.GetValue(Random.Range(0, skillIDs.Length));

        //    _skillChoiceSlotArr[i].SetSlotInfos(skillID);
        //}
    }


    // �˾� ����
    public void OpenPopup()
    {
        gameObject.SetActive(true);
    }


    // �˾� �ݱ�
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
