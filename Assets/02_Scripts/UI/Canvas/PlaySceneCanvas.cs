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

    [Title("Popups")]
    [SerializeField]
    public SkillSelectPopup SkillSelectPopup { get; set; }    // ��ų ���� �˾�    
    [SerializeField]
    public ResultPopup ResultPopup { get; set; }              // ��� �˾�

    [Title("UIs")]    
    [SerializeField]
    public PlayTimeUI PlayTimeUI { get; set; }                  // �÷��� Ÿ�� UI

    [SerializeField]
    public SkillInvenUI SkillInvenUI { get; set; }              // ��ų �κ��丮 UI

    [SerializeField]
    public GoldInvenUI GoldInvenUI { get; set; }                // ��� �κ��丮 UI

    [SerializeField]
    public EXPBarUI EXPBarUI { get; set; }                        // EXP �������� UI

    [SerializeField]
    public BossHPBarUI BossHPBarUI { get; set; }                // ���� HP �� UI

    [SerializeField]
    public RewardBoxPopup RewardBoxPopup { get; set; }          // ���� ���� �˾�

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
        SkillSelectPopup.OpenPopup();
        ResultPopup.ClosePopup();
    }
}
