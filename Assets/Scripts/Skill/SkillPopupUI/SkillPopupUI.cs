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
    private List<Skill_Base> _availableSkillList = new List<Skill_Base>()
    {
        new Skill_SlashAttack(),
        new Skill_Boomerang(),
        new Skill_Sniper()
    };

    //private void Awake()
    //{
    //    _availableSkillList.Add(new Skill_SlashAttack(DataManager.Instance.SkillData_as_SkillDataDict<SkillData_SlashAttack>(SkillID.SlashAttack, 0)));
    //    _availableSkillList.Add(new Skill_Boomerang(DataManager.Instance.SkillData_as_SkillDataDict<SkillData_Boomerang>(SkillID.Boomerang, 0)));
    //    _availableSkillList.Add(new Skill_Sniper(DataManager.Instance.SkillData_as_SkillDataDict<SkillData_Sniper>(SkillID.Sniper, 0)));

    //}


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
        foreach (Skill_Base skill in _availableSkillList)
        {
            GameObject skillButtonPrefab = Instantiate(_skillButtonPrefab, _buttonParent);
            Button skillButton = skillButtonPrefab.GetComponent<Button>();
            TextMeshProUGUI skillText = skillButtonPrefab.GetComponentInChildren<TextMeshProUGUI>();

            // �̹� ��ų�� ������ �ִٸ� ���� ǥ��
            if (_playerSkillManager.HasSkill(skill))
            {
                int currentLevel = _playerSkillManager.GetSkillLevel(skill);
                skillText.text = $"{skill.Name} (Level {currentLevel + 1})";
            }
            // ������ ���� �ʴٸ� ��ų �̸��� ǥ��
            else
            {
                skillText.text = skill.Name;
            }

            // ��ư Ŭ�� �� ��ų ����
            skillButton.onClick.AddListener(() => OnSkillSelected(skill));

            // �˾� �ѱ�
            gameObject.SetActive(true);
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
