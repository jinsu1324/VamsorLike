using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// PlayScene에 필요한 글로벌한 것들 관리자 : 이번게임에 플레이하고있는 영웅 / 게임시작 여부 / 게임시작 / 영웅선택 팝업 
public class PlaySceneManager : SerializedMonoBehaviour
{
    #region 싱글톤_씬이동x
    private static PlaySceneManager _instance;

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

    public static PlaySceneManager Instance
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

    public HeroObj MyHeroObj { get; set; }                          // 내가 이번 게임에 선택한 영웅
    public bool IsGameStart { get; set; }                           // 게임 시작 되었는지

    [Title("Managers")]
    [SerializeField]
    public EnemySpawner EnemySpawner { get; set; }                 // 적 스포너

    [SerializeField]
    public ItemSpawner ItemSpawner { get; set; }                   // 아이템 스포너

    [SerializeField]
    public EnemyManager EnemyManager { get; set; }                 // 적 매니저

    [SerializeField]
    public ItemManager ItemManager { get; set; }                   // 아이템 매니저

    [SerializeField]
    public WaveManager WaveManager { get; set; }                   // 웨이브매니저

    [SerializeField]
    public SkillManager SkillManager { get; set; }                 // 스킬매니저

    [SerializeField]
    public LevelManager LevelManager { get; set; }                 // 레벨매니저

    [SerializeField]
    public GoldManager GoldManager { get; set; }                   // 골드매니저

    [SerializeField]
    public AchivementManager AchivementManager { get; set; }       // 플레이 통계, 업적
    
    
    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        // 페이드 아웃
        PlaySceneCanvas.Instance.LoadingViews.FadeOut();

        // 선택된 영웅 셋팅
        MyHeroObjSetting();

        // 게임시작 true 로
        IsGameStartChange(true);
    }

    /// <summary>
    /// 이번게임영웅으로 선택된 영웅 셋팅 및 스폰
    /// </summary>
    private void MyHeroObjSetting()
    {
        // 내가 이번 게임에 선택한 영웅 ID 받아옴
        HeroID myHeroID = GameManager.Instance.myHeroID;

        // 선택한 영웅을 이번게임의 영웅으로 할당
        MyHeroObj = ResourceManager.Instance.HeroObjectDict[myHeroID];

        // 이번게임으로 선택된 영웅을 필드에 스폰
        MyHeroObj = Instantiate(ResourceManager.Instance.HeroObjectDict[myHeroID], new Vector3(0, 0, 0), Quaternion.identity);

        // 이번게임으로 선택된 영웅 데이터 셋팅
        MyHeroObj.DataSetting();

        // 따라다닐 카메라 셋팅
        Camera.main.GetComponent<CameraFollow>().SetFollowTarget(MyHeroObj);
    }    

    /// <summary>
    /// 게임시작 bool 파라미터로 변경
    /// </summary>
    public void IsGameStartChange(bool state)
    {        
        IsGameStart = state;
    }  







    // 치트
    public void DeadCheat()
    {
        MyHeroObj.Death();
    }
}
