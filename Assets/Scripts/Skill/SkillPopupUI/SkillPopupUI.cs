using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPopupUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _skillButtonPrefab;
    [SerializeField]
    private Transform _buttonParent;

    private PlayerSkillManager _playerSkillManager = new PlayerSkillManager();

    private List<Skill_Base> _availableSkills = new List<Skill_Base>
    {
        new Skill_SlashAttack(),
        new Skill_Sniper(),
        new Skill_Boomerang(),
    };


    /// <summary>
    /// �˾� ���� �Լ�
    /// </summary>
    public void OpenSkillPopup()
    {
        gameObject.SetActive(true);

        foreach (Transform child in _buttonParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Skill_Base skill in _availableSkills)
        {
            GameObject skillButtonPrefab = Instantiate(_skillButtonPrefab, _buttonParent);
            Button skillButton = skillButtonPrefab.GetComponent<Button>();
            TextMeshProUGUI skillText = skillButtonPrefab.GetComponentInChildren<TextMeshProUGUI>();

            if (_playerSkillManager.HasSkill(skill))
            {
                int currentLevel = _playerSkillManager.GetSkillLevel(skill);
                skillText.text = $"{skill.Name} (Level {currentLevel + 1})";
            }
            else
            {
                skillText.text = skill.Name;
            }

            // ��ư Ŭ�� �� ��ų ����
            skillButton.onClick.AddListener(() => OnSkillSelected(skill));
        }
    }

    /// <summary>
    /// ��ų ���� �� ȣ��Ǵ� �Լ�
    /// </summary>
    public void OnSkillSelected(Skill_Base skill)
    {
        _playerSkillManager.AddSkill(skill);

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
