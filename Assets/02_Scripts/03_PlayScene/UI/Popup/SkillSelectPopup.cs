using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ų ���� �˾� UI
/// </summary>
public class SkillSelectPopup : SerializedMonoBehaviour
{

    [SerializeField]
    private List<SkillSelectPopup_Slot> _slotList;

   

    public void InitSlotList()
    {
        foreach (SkillSelectPopup_Slot slot in _slotList) 
        {

            // ���� �ʱ�ȭ�� ������ ä��ٰ� ���� ��ų ID�� ������ ���� �ʱ�ȭ�� �ǳʶ�
            if (PlaySceneManager.Instance.SkillManager.RemainSkillCount == 0)
            {
                Debug.LogWarning("��� ��ų�� ���õǾ� �� �̻� ������ �ʱ�ȭ�� �� �����ϴ�.");
                slot.HideSlot();
                break;
            }
            else
            {
                SkillID randomSkillID = PlaySceneManager.Instance.SkillManager.RandomUniqueSkillID();

                slot.Initialize(randomSkillID, this);
            }


            
        }
        
    }


    /// <summary>
    /// �˾� ����
    /// </summary>
    public void OpenPopup()
    {
        // ���� 1�� ä�� ������ų�� ������ �˾��� ���� ����
        if (PlaySceneManager.Instance.SkillManager.RemainSkillCount == 0)
        {
            Debug.LogWarning("��� ��ų�� ȹ���Ͽ� �˾��� �����մϴ�.");
            // �ٸ� ���� ���ָ� �ɵ�?
            // �ּ��� ���� ����...
            return;
        }


        PlaySceneManager.Instance.IsGameStartChange(false);

        InitSlotList();

        gameObject.SetActive(true);
    }

    /// <summary>
    /// �˾� �ݱ�
    /// </summary>
    public void ClosePopup()
    {
        // ���� ��ųID ������ �ٽ� �ʱ�ȭ
        PlaySceneManager.Instance.SkillManager.ResetRemainSkillIDList();

        gameObject.SetActive(false);
    }
}
