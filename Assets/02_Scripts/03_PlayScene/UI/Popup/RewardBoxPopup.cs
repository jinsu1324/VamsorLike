using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardBoxPopup : MonoBehaviour
{
    [SerializeField]
    private Image _skillIconImage;                  // ��ų ������
    [SerializeField]
    private TextMeshProUGUI _skillNameText;         // ��ų �̸� �ؽ�Ʈ
    [SerializeField]
    private TextMeshProUGUI _skillLevelText;        // ��ų ���� �ؽ�Ʈ
    [SerializeField]
    private TextMeshProUGUI _skillDescText;         // ��ų ���� �ؽ�Ʈ
    [SerializeField]
    private GameObject _rewardGO;                   // ���� ���ӿ�����Ʈ

    
    /// <summary>
    /// �˾� ���� �� �ʱ�ȭ
    /// </summary>
    public void Initialize()
    {
        // ���� ��ų�� 1���� ������ �˾��� ���� ����
        if (PlaySceneManager.Instance.SkillManager.RemainSkillCount == 0)
        {
            Debug.Log("��� ��ų�� ȹ���Ͽ� �˾��� �����մϴ�.");

            // ��� �������� �ϴ�
            PlaySceneCanvas.Instance.PlayAchivementUI.AddGold(5);
            PlaySceneCanvas.Instance.PlayAchivementUI.MoveIconManager.StartMove_IconToTargetIcon(PlaySceneManager.Instance.MyHeroObj.transform);
            PlaySceneCanvas.Instance.PlayAchivementUI.MoveIconManager.StartMove_IconToTargetIcon(PlaySceneManager.Instance.MyHeroObj.transform);
            PlaySceneCanvas.Instance.PlayAchivementUI.MoveIconManager.StartMove_IconToTargetIcon(PlaySceneManager.Instance.MyHeroObj.transform);
            PlaySceneCanvas.Instance.PlayAchivementUI.MoveIconManager.StartMove_IconToTargetIcon(PlaySceneManager.Instance.MyHeroObj.transform);
            PlaySceneCanvas.Instance.PlayAchivementUI.MoveIconManager.StartMove_IconToTargetIcon(PlaySceneManager.Instance.MyHeroObj.transform);

            return;
        }



        PlaySceneManager.Instance.IsGameStartChange(false);

        SettingRewardSkill();

        _rewardGO.SetActive(false);

        gameObject.SetActive(true);
    }

    /// <summary>
    /// ���� ���ӿ�����Ʈ ����
    /// </summary>
    public void OpenRewardGO()
    {
        _rewardGO.SetActive(true);

        Invoke("ClosePopup", 3.0f);
    }

    /// <summary>
    /// �˾� ����
    /// </summary>
    public void ClosePopup()
    {
        _rewardGO.SetActive(false);
        gameObject.SetActive(false);

        PlaySceneManager.Instance.IsGameStartChange(true);
    }

    /// <summary>
    /// ������ų �������� �κ��� �߰� �� UI�� ���� �����ϴ� �Լ�
    /// </summary>
    private void SettingRewardSkill()
    {
        SkillManager skillManager = PlaySceneManager.Instance.SkillManager;

        // ��ġ�� �ʴ� ������ ��ųID�� �޾ƿ�
        SkillID skillID = skillManager.RandomUniqueSkillID();

        // �ش罺ų ������ �������� ���
        int skillLevel = skillManager.GetSkillLevel(skillID);
        int nextLevel = skillLevel + 1;

        // �� ���� ���� ��ų������ ��������
        SkillData skillData = skillManager.GetSkillData_by_SkillIDLevel(skillID, nextLevel);

        // UI�� ǥ��
        _skillIconImage.sprite = ResourceManager.Instance.SkillIconDict[skillID];
        _skillNameText.text = skillData.Name;
        _skillDescText.text = skillData.Desc;
        if (nextLevel == 1)
        {
            _skillLevelText.text = "�ű�!";
        }
        else
        {
            _skillLevelText.text = $"Lv.{nextLevel.ToString()}";
        }

        // �޾ƿ� ��ų �κ��丮�� �߰�
        skillManager.AddSkill(skillID);

        // ��ų Ǯ �ʱ�ȭ
        skillManager.ResetRemainSkillIDList();
    }
}
