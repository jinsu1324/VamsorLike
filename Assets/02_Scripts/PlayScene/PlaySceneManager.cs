using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// PlayScene에 필요한 글로벌한 것들 관리자 : 이번게임에 플레이하고있는 영웅 / 게임시작 여부 / 게임시작 / 영웅선택 팝업 
public class PlaySceneManager : SerializedMonoBehaviour
{
    #region 싱글톤
    private static PlaySceneManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
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

    public static HeroObject ThisGameHeroObject { get; set; }   // 내가 이번 게임에 선택한 영웅
    public bool IsGameStart { get; set; }                       // 영웅 선택해서 게임 시작 되었는지

    [SerializeField]
    public PlaySceneCanvas PlaySceneCanvas { get; set; }        // 플레이씬 캔버스 

    private float _playTime = 0.0f;                             // 플레이타임


    /// <summary>
    /// Update 함수
    /// </summary>
    private void Update()
    {        
        PlayTimeCalculate_UIRefresh();
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
    }

    /// <summary>
    /// 게임시작 bool 파라미터로 변경
    /// </summary>
    public void IsGameStartChange(bool state)
    {        
        IsGameStart = state;
    }

    /// <summary>
    /// 이번게임영웅으로 선택된 영웅 셋팅 및 스폰
    /// </summary>
    public void ThisGameHeroSetting(HeroID selectHeroID)
    {
        // 선택한 영웅을 이번게임의 영웅으로 할당
        ThisGameHeroObject = HeroManager.Instance.HeroObjectDict[selectHeroID];

        // 이번게임으로 선택된 영웅을 필드에 스폰
        ThisGameHeroObject = Instantiate(HeroManager.Instance.HeroObjectDict[selectHeroID], new Vector3(0, 0, 0), Quaternion.identity);

        // 이번게임으로 선택된 영웅 데이터 셋팅
        ThisGameHeroObject.DataSetting();
    }
}
