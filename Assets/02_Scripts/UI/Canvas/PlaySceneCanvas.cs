using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneCanvas : SerializedMonoBehaviour
{
    [Title("Popups")]
    [SerializeField]
    public SkillSelectPopup SkillSelectPopup { get; set; }    // ��ų ���� �˾�    
    [SerializeField]
    public ResultPopup ResultPopup { get; set; }              // ��� �˾�

    [Title("UIs")]
    [SerializeField]
    public ObjectPool DamageTextsPool { get; set; }            // ������ �ؽ�Ʈ UI ������Ʈ Ǯ

    [SerializeField]
    public PlayTimeUI PlayTimeUI { get; set; }                  // �÷��� Ÿ�� UI

    [SerializeField]
    public SkillInvenUI SkillInvenUI { get; set; }              // ��ų �κ��丮 UI

    [SerializeField]
    public BossHPBarUI BossHPBarUI { get; set; }                // ���� HP �� UI

    [Title("Interactions")]
    [SerializeField]
    public Joystick Joystick { get; set; }                    // ���̽�ƽ


    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        PopupsInitialize();
    }

    /// <summary>
    /// �� �˾��� ON OFF �ʱ�ȭ
    /// </summary>
    private void PopupsInitialize()
    {
        SkillSelectPopup.OpenPopup();
        ResultPopup.ClosePopup();
    }
}
