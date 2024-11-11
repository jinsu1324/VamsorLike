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
    private List<SkillSelectPopup_Slot> _slotList;              // ��ų ���� ����Ʈ

    [SerializeField]
    private GameObject _firstPopParticle;                       // ó�� �� ����Ʈ

    /// <summary>
    /// ��ų���� ����Ʈ �ʱ�ȭ
    /// </summary>
    public void InitSlotList()
    {
        foreach (SkillSelectPopup_Slot slot in _slotList) 
        {

            // ���� �ϳ��� �ʱ�ȭ�ٰ� ���� ��ų ID�� ������, ���� �ʱ�ȭ�� �ǳʶ� (���� ������ų�� 1~2���ΰ��)
            if (PlaySceneManager.Instance.SkillManager.RemainSkillCount == 0)
            {
                Debug.Log("��� ��ų�� ���õǾ� �� �̻� ������ �ʱ�ȭ�� �� �����ϴ�.");
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
        // ���� ��ų�� 1���� ������ �˾��� ���� ����
        if (PlaySceneManager.Instance.SkillManager.RemainSkillCount == 0)
        {
            Debug.Log("��� ��ų�� ȹ���Ͽ� �˾��� �����մϴ�.");
            
            // �ٸ� ����
            
            return;
        }

        // �������� ���߱�
        PlaySceneManager.Instance.IsGameStartChange(false);

        // ���� �ʱ�ȭ
        InitSlotList();

        // �˾� �ѱ�
        gameObject.SetActive(true);

        // ��ƼŬ ������Ʈ �ѱ�
        _firstPopParticle.SetActive(true);
    }

    /// <summary>
    /// �˾� �ݱ�
    /// </summary>
    public void ClosePopup()
    {
        // ������ųID ������ �ٽ� �ʱ�ȭ
        PlaySceneManager.Instance.SkillManager.ResetRemainSkillIDList();

        // �˾� �ݱ�
        gameObject.SetActive(false);
    }
}
