using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardBoxPopup : MonoBehaviour
{
    private Animator _animator;                     // �ִϸ�����

    [SerializeField]
    private Image _skillIconImage;                  // ��ų ������
    [SerializeField]
    private TextMeshProUGUI _skillNameText;         // ��ų �̸� �ؽ�Ʈ
    [SerializeField]
    private TextMeshProUGUI _skillLevelText;        // ��ų ���� �ؽ�Ʈ
    [SerializeField]
    private TextMeshProUGUI _skillDescText;         // ��ų ���� �ؽ�Ʈ

    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        // ������� ����� �� �̺�Ʈ ���
        RewardBoxItem.OnGetRewardBox += Initialize_Popup;
    }

    /// <summary>
    /// �˾� ���� �� �ʱ�ȭ
    /// </summary>
    public void Initialize_Popup()
    {
        PlaySceneManager.Instance.IsGameStartChange(false);

        _animator = GetComponent<Animator>();
        SetAndAdd_RandomRewardSkill();
        gameObject.SetActive(true);
    }

    /// <summary>
    /// �ִϸ����� IsOpenReady true�� ����
    /// </summary>
    public void ReadyComplete()
    {
        _animator.SetBool("isOpenReady", true);
    }

    /// <summary>
    /// �˾� ���� + isOpenReady false�� �����ϴ� �Լ�
    /// </summary>
    public void End_Popup()
    {
        _animator.SetBool("isOpenReady", false);
        gameObject.SetActive(false);

        PlaySceneManager.Instance.IsGameStartChange(true);
    }

    /// <summary>
    /// ������ų �������� �κ��� �߰� �� UI�� ���� �����ϴ� �Լ�
    /// </summary>
    private void SetAndAdd_RandomRewardSkill()
    {
        SkillManager skillManager = SkillManager.Instance;

        // ������ ��ųID�� �޾ƿ�
        SkillID randomSkillID = skillManager.RandomSkillID();
        
        // �޾ƿ� ��ų �κ��丮�� �߰�
        skillManager.AddSkill(randomSkillID);
        
        // �޾ƿ� ��ų�� �˾� UI�� ���� ���� 
        Sprite skillIcon = skillManager.GetSkillIcon(randomSkillID);
        string skillName = skillManager.GetSkillName(randomSkillID);
        int skillLevel = skillManager.GetSkillLevel(randomSkillID);
        string skillDesc = skillManager.GetSkillDesc(randomSkillID);

        _skillIconImage.sprite = skillIcon;
        _skillNameText.text = skillName;
        _skillLevelText.text = $"Level {skillLevel}";
        _skillDescText.text = skillDesc;
    }

    /// <summary>
    /// ��Ȱ��ȭ �� �̺�Ʈ ���� (���� ���� �� �����ǵ��� ��)
    /// </summary>
    private void OnDisable()
    {
        RewardBoxItem.OnGetRewardBox -= Initialize_Popup;
    }
}
