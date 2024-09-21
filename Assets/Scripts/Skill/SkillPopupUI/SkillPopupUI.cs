using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ų ���� �˾� UI
/// </summary>
public class SkillPopupUI : MonoBehaviour
{
    // ��ų ��ư ������
    [SerializeField]
    private GameObject _skillButtonPrefab;
    
    // ��ư �θ�
    [SerializeField]
    private Transform _buttonParent;

    // �÷��̾� ��ų �κ��丮�� �ִ� ��ų�Ŵ���
    private PlayerSkillManager _playerSkillManager = new PlayerSkillManager();

    // ��� ������ ��ų ����Ʈ
    private List<SkillID> _availableSkillList = new List<SkillID>()
    {
        SkillID.SlashAttack,
        SkillID.Boomerang,
        SkillID.Sniper
    };


    /// <summary>
    /// �˾� ����
    /// </summary>
    public void OpenSkillPopup()
    {
        // �ڽ� ��ư�� ����
        foreach (Transform child in _buttonParent)
        {
            Destroy(child.gameObject);
        }

        // ��� ������ ��ų���� ��ư���� ����
        foreach (SkillID skillID in _availableSkillList)
        {
            int skillLevel = _playerSkillManager.GetSkillLevel(skillID);
            var skillData = DataManager.Instance.SkillData_as_SkillDataDict<SkillData_Base>(skillID, skillLevel);

            GameObject skillButtonPrefab = Instantiate(_skillButtonPrefab, _buttonParent);
            Button skillButton = skillButtonPrefab.GetComponent<Button>();
            TextMeshProUGUI skillText = skillButtonPrefab.GetComponentInChildren<TextMeshProUGUI>();

            // �̹� ��ų�� ������ �ִٸ� ���� ǥ��
            if (_playerSkillManager.HasSkill(skillID))
            {
                int currentLevel = _playerSkillManager.GetSkillLevel(skillID);
                skillText.text = $"{skillData.Name} (Level {currentLevel + 1})";
            }
            // ������ ���� �ʴٸ� ��ų �̸��� ǥ��
            else
            {
                skillText.text = skillData.Name;
            }

            // ��ư Ŭ�� �� ��ų ����
            skillButton.onClick.AddListener(() => OnSkillSelected(skillID));

            // �˾� �ѱ�
            gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// ��ų ���� �� ȣ��Ǵ� �Լ�
    /// </summary>
    public void OnSkillSelected(SkillID skillID)
    {
        _playerSkillManager.AddSkill(skillID);

        // ���ӽ��� �ȵǾ��־��ٸ�
        if (PlaySceneManager.Instance.IsGameStart == false)
        {
            // ���ӽ����� true��
            PlaySceneManager.Instance.IsGameStartChange(true);

            // ���� ���� ����
            MonsterSpawner.Instance.StartMonsterSpawn();
        }

        CloseSkillPopup();
    }

    /// <summary>
    /// �˾� �ݱ�
    /// </summary>
    public void CloseSkillPopup()
    {
        gameObject.SetActive(false);
    }
}
