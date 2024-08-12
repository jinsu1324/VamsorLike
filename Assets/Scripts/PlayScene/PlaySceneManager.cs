using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Title("Managers", bold: false)]
    // 몬스터 데이터 매니저
    [SerializeField]
    private MonsterDataManager _monsterDataManager;
    public MonsterDataManager MonsterDataManager { get { return _monsterDataManager; } }

    // 영웅 데이터 매니저
    [SerializeField]
    private HeroDataManager _heroDataManager;
    public HeroDataManager HeroDataManager { get { return _heroDataManager; } }

    // 몬스터 스폰 매니저
    [SerializeField]
    private MonsterSpawnManager _monsterSpawnManager;
    public MonsterSpawnManager MonsterSpawnManager { get { return _monsterSpawnManager; } }

    [Title("Popups", bold: false)]
    // 영웅 선택 팝업
    [SerializeField]
    private HeroSelectPopup _heroSelectPopup;
    public HeroSelectPopup HeroSelectPopup { get { return _heroSelectPopup; } }

    [Title("")]
    // 내가 이번 게임에 선택한 영웅
    private HeroObject _thisGameHeroObject;
    public HeroObject ThisGameHeroObject { get{ return _thisGameHeroObject; } }

    // 영웅 선택해서 게임 시작 되었는지
    private bool _isGameStart = false;
    public bool IsGameStart { get { return _isGameStart; } }


    // 영웅 선택 완료해서 게임 시작
    public void PlayStart(HeroData SelectedHeroData)
    {       
        // 파라미터로 받아온 선택된 영웅데이터를, 내가 이번 게임에 선택한 영웅에 넣어줌
        HeroID heroID = (HeroID)Enum.Parse(typeof(HeroID), SelectedHeroData.Id);
        _thisGameHeroObject = _heroDataManager.HeroObjectDict[heroID];

        // 필드에 스폰하고 스탯도 넣어줌
        _thisGameHeroObject = Instantiate(_heroDataManager.HeroObjectDict[heroID]);
        _thisGameHeroObject.Spawn();

        // 게임시작 bool 을 true로
        _monsterSpawnManager.IsSpawned = true;

        Debug.Log($"{_thisGameHeroObject.Name}로 게임을 시작합니다!!!!");
    }
}
