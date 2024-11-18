using DarkPixelRPGUI.Scripts.UI.Equipment;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectPopup_Slot : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nameText;              // ��ų �̸� �ؽ�Ʈ

    [SerializeField]
    private TextMeshProUGUI _levelText;             // ��ų ���� �ؽ�Ʈ

    [SerializeField]
    private TextMeshProUGUI _descText;              // ��ų ���� �ؽ�Ʈ

    [SerializeField]
    private Image _iconImage;                       // ��ų ������ �̹���

    [SerializeField]
    private Button _slotButton;                     // ��ų ���� ��ư

    private SkillManager _skillManager;             // ��ų �Ŵ��� ���� ����
    private SkillSelectPopup _skillSelectPopup;     // ��ų ���� �˾� ���� ����

    /// <summary>
    /// ���� �ʱ�ȭ
    /// </summary>
    public void Initialize(SkillID skillID, SkillSelectPopup skillSelectPopup)
    {
        // �ʿ��� ������ �Ҵ�
        _skillManager = PlaySceneManager.Instance.SkillManager;
        _skillSelectPopup = skillSelectPopup;

        // �ش罺ų ������ �������� ���
        int skillLevel = _skillManager.GetSkillLevel(skillID);
        int nextLevel = skillLevel + 1;

        // �� ���� ���� ��ų������ ��������
        SkillData skillData = _skillManager.GetSkillData_by_SkillIDLevel(skillID, nextLevel);

        // UI�� ǥ��
        _iconImage.sprite = ResourceManager.Instance.SkillIconDict[skillID];
        _nameText.text = skillData.Name;
        _descText.text = skillData.Desc;
        if (nextLevel == 1)
        {
            _levelText.text = "�ű�!";
        }
        else
        {
            _levelText.text = $"Lv.{nextLevel.ToString()}";
        }

        // ��ư�� �̺�Ʈ ��� �� �ߺ� �̺�Ʈ ����
        _slotButton.onClick.RemoveAllListeners();
        _slotButton.onClick.AddListener(() => OnSkillSelected(skillID));

        // ���� �ѱ�
        gameObject.SetActive(true);
    }

    /// <summary>
    /// ��ų���� ���õǾ����� ����� �Լ�
    /// </summary>
    private void OnSkillSelected(SkillID skillID)
    {
        // ��ų �߰�
        _skillManager.AddSkill(skillID);

        // ���� ����
        bool isGameStart = PlaySceneManager.Instance.IsGameStart;
        if (isGameStart == false)
            PlaySceneManager.Instance.IsGameStartChange(true);

        // ��ų���� �˾� �ݱ�
        _skillSelectPopup.ClosePopup();

        // ����� ���
        AudioManager.Instance.PlaySFX(SFXType.ButtonClick);
    }

    /// <summary>
    /// ���� �ݱ�
    /// </summary>
    public void HideSlot()
    {
        gameObject.SetActive(false);
    }
}
