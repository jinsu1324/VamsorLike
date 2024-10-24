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

    public HeroObj MyHeroObj { get; set; }                       // 내가 이번 게임에 선택한 영웅
    public bool IsGameStart { get; set; }                           // 게임 시작 되었는지

    [SerializeField]
    public PlaySceneCanvas PlaySceneCanvas { get; set; }            // 플레이씬 캔버스
    
    [SerializeField]
    public PlayAchivement PlayAchivement { get; set; }              // 플레이 통계, 업적

    public int StageLevel { get; set; } = 1;                        // 스테이지 레벨 
    public int MaxStageLevel { get; set; } = 4;                     // 최대 스테이지 레벨  
    public float StageLevelUpIntervelTime { get; set; } = 10.0f;    // 스테이지 레벨업 간격

    private float _playTime = 0.0f;                                 // 플레이타임
    
   
    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        MyHeroObjSetting();
    }

    /// <summary>
    /// Update 함수
    /// </summary>
    private void Update()
    {        
        PlayTimeCalculate_UIRefresh();
    }

    /// <summary>
    /// 이번게임영웅으로 선택된 영웅 셋팅 및 스폰
    /// </summary>
    private void MyHeroObjSetting()
    {
        HeroID myHeroID = GameManager.Instance.myHeroID;

        // 선택한 영웅을 이번게임의 영웅으로 할당
        MyHeroObj = ObjectManager.Instance.HeroObjectDict[myHeroID];

        // 이번게임으로 선택된 영웅을 필드에 스폰
        MyHeroObj = Instantiate(ObjectManager.Instance.HeroObjectDict[myHeroID], new Vector3(0, 0, 0), Quaternion.identity);

        // 이번게임으로 선택된 영웅 데이터 셋팅
        MyHeroObj.DataSetting();

        // 따라다닐 카메라 셋팅
        Camera.main.GetComponent<CameraFollow>().SetFollowTarget(MyHeroObj);
    }

    /// <summary>
    /// 플레이타임 계산 + 갱신
    /// </summary>
    private void PlayTimeCalculate_UIRefresh()
    {
        if (IsGameStart == false)
            return;

        _playTime += Time.deltaTime;

        int minute = Mathf.FloorToInt(_playTime / 60F);
        int second = Mathf.FloorToInt(_playTime % 60F);

        PlaySceneCanvas.PlayTimeUI.RefreshUIText(minute, second);

        // 플레이타임 체크해서 스테이지 레벨업
        if (_playTime >= StageLevel * StageLevelUpIntervelTime)
            ChangeStageLevel();
    }

    /// <summary>
    /// 게임시작 bool 파라미터로 변경
    /// </summary>
    public void IsGameStartChange(bool state)
    {        
        IsGameStart = state;
    }    

    /// <summary>
    /// 스테이지 레벨업
    /// </summary>
    public void ChangeStageLevel()
    {
        if (StageLevel >= MaxStageLevel)
        {
            Debug.Log("최대 스테이지 레벨입니다.");
            return;
        }

        StageLevel++;
        Debug.Log($"스테이지 레벨업! : {StageLevel}");

        if (StageLevel == 2)
        {
            EnemySpawner.Instance.BossSpawn();
        }        
    }


    public void DeadCheat()
    {
        MyHeroObj.Death();
    }
}
