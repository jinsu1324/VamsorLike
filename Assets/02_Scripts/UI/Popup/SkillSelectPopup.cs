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
    // ��ų ��ư ������
    [SerializeField]
    private GameObject _skillButtonPrefab;
    
    // ��ư �θ�
    [SerializeField]
    private Transform _buttonParent;

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
    public void OpenPopup()
    {
        // ���ӽ����� false��
        PlaySceneManager.Instance.IsGameStartChange(false);

        // �ڽ� ��ư�� ����
        foreach (Transform child in _buttonParent)
        {
            Destroy(child.gameObject);
        }

        // ��� ������ ��ų���� ��ư���� ����
        foreach (SkillID skillID in _availableSkillList)
        {
            int skillLevel = SkillManager.Instance.GetSkillLevel(skillID);
            var skillData = SkillManager.Instance.SkillData_as_Dict<SkillData_Base>(skillID, skillLevel);

            GameObject skillButtonPrefab = Instantiate(_skillButtonPrefab, _buttonParent);
            Button skillButton = skillButtonPrefab.GetComponent<Button>();
            TextMeshProUGUI skillText = skillButtonPrefab.GetComponentInChildren<TextMeshProUGUI>();

            // �̹� ��ų�� ������ �ִٸ� ���� ǥ��
            if (SkillManager.Instance.HasSkill(skillID))
            {
                int currentLevel = SkillManager.Instance.GetSkillLevel(skillID);
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
        SkillManager.Instance.AddSkill(skillID);

        // ���ӽ��� �ȵǾ��־��ٸ�
        if (PlaySceneManager.Instance.IsGameStart == false)
        {
            // ���ӽ����� true��
            PlaySceneManager.Instance.IsGameStartChange(true);

            // ���� ���� ����
            EnemySpawner.Instance.StartMonsterSpawn();
        }

        ClosePopup();
    }

    /// <summary>
    /// �˾� �ݱ�
    /// </summary>
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
