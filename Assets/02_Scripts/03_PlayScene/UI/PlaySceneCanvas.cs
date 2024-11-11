using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneCanvas : SerializedMonoBehaviour
{
    #region �̱���_���̵�x
    private static PlaySceneCanvas _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static PlaySceneCanvas Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }

            return _instance;
        }
    }
    #endregion

    [Title("UICam")]
    [SerializeField]
    public Camera UICamera { get; set; }                      // UI ī�޶�

    [Title("Popups")]
    [SerializeField]
    public SkillSelectPopup SkillSelectPopup { get; set; }    // ��ų ���� �˾�    
    [SerializeField]
    public ResultPopup ResultPopup { get; set; }              // ��� �˾�

    [SerializeField]
    public PausePopup PausePopup { get; set; }                // �Ͻ����� �˾�

    [Title("UIs")]    
    [SerializeField]
    public PlayTimeUI PlayTimeUI { get; set; }                  // �÷��� Ÿ�� UI

    [SerializeField]
    public SkillInvenUI SkillInvenUI { get; set; }              // ��ų �κ��丮 UI

    [SerializeField]
    public PlayAchivementUI PlayAchivementUI { get; set; }          // �÷��� ���� UI

    [SerializeField]
    public EXPBarUI EXPBarUI { get; set; }                        // EXP �������� UI

    [SerializeField]
    public BossHPBarUI BossHPBarUI { get; set; }                // ���� HP �� UI

    [SerializeField]
    public RewardBoxPopup RewardBoxPopup { get; set; }          // ���� ���� �˾�

    [Title("Views")]
    [SerializeField]
    public FadeInOutController FadeInOutController { get; set; }              // �ε� ��

    [SerializeField]
    public CautionView CautionView { get; set; }                // CautionView

    [Title("Interactions")]
    [SerializeField]
    public Joystick Joystick { get; set; }                    // ���̽�ƽ

    [Title("Pools")]
    [SerializeField]
    public ObjectPool DamageTextsPool { get; set; }            // ������ �ؽ�Ʈ UI ������Ʈ Ǯ

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
        SkillSelectPopup.ClosePopup();
        ResultPopup.ClosePopup();
    }
}
