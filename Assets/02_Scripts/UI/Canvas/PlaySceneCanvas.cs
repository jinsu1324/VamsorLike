using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneCanvas : SerializedMonoBehaviour
{
    #region 싱글톤_씬이동x
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
    public SkillSelectPopup SkillSelectPopup { get; set; }    // 스킬 선택 팝업    
    [SerializeField]
    public ResultPopup ResultPopup { get; set; }              // 결과 팝업

    [Title("UIs")]    
    [SerializeField]
    public PlayTimeUI PlayTimeUI { get; set; }                  // 플레이 타임 UI

    [SerializeField]
    public SkillInvenUI SkillInvenUI { get; set; }              // 스킬 인벤토리 UI

    [SerializeField]
    public GoldInvenUI GoldInvenUI { get; set; }                // 골드 인벤토리 UI

    [SerializeField]
    public EXPBarUI EXPBarUI { get; set; }                        // EXP 게이지바 UI

    [SerializeField]
    public BossHPBarUI BossHPBarUI { get; set; }                // 보스 HP 바 UI

    [SerializeField]
    public RewardBoxPopup RewardBoxPopup { get; set; }          // 보상 상자 팝업

    [Title("Interactions")]
    [SerializeField]
    public Joystick Joystick { get; set; }                    // 조이스틱

    [Title("Pools")]
    [SerializeField]
    public ObjectPool DamageTextsPool { get; set; }            // 데미지 텍스트 UI 오브젝트 풀

    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        PopupsInitialize();
    }

    /// <summary>
    /// 씬 팝업들 ON OFF 초기화
    /// </summary>
    private void PopupsInitialize()
    {
        SkillSelectPopup.OpenPopup();
        ResultPopup.ClosePopup();
    }
}
