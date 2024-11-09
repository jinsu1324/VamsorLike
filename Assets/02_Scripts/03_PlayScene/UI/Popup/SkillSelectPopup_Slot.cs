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
    private TextMeshProUGUI _nameText;

    [SerializeField]
    private TextMeshProUGUI _levelText;

    [SerializeField]
    private TextMeshProUGUI _descText;

    [SerializeField]
    private Button _slotButton;

    private SkillManager _skillManager;
    private SkillSelectPopup _skillSelectPopup;

    public void Initialize(SkillID skillID, SkillSelectPopup skillSelectPopup)
    {
        

        _skillManager = PlaySceneManager.Instance.SkillManager;
        _skillSelectPopup = skillSelectPopup;

        int skillLevel = _skillManager.GetSkillLevel(skillID);




        int nextLevel = skillLevel + 1;

        SkillData skillData = _skillManager.GetSkillData_by_SkillIDLevel(skillID, nextLevel);


        _nameText.text = skillData.Name;
        _descText.text = skillData.Desc;

        _levelText.text = $"Lv.{nextLevel.ToString()}";

        //if (skillLevel == 0)
        //{

        //    _levelText.text = "신규!";

        //}
        //else
        //{

        //    _levelText.text = $"Lv.{nextLevel.ToString()}";
        //}

        // 중복 이벤트 방지
        _slotButton.onClick.RemoveAllListeners();
        _slotButton.onClick.AddListener(() => OnSkillSelected(skillID));

        gameObject.SetActive(true);
    }

    private void OnSkillSelected(SkillID skillID)
    {
       
        _skillManager.AddSkill(skillID);

        bool isGameStart = PlaySceneManager.Instance.IsGameStart;

        if (isGameStart == false)
            PlaySceneManager.Instance.IsGameStartChange(true);

        _skillSelectPopup.ClosePopup();
    }

    public void HideSlot()
    {
        gameObject.SetActive(false);
    }
}
