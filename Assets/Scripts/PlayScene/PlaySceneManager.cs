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

    // 내가 이번 게임에 선택한 영웅
    private static HeroObject _thisGameHeroObject;
    public static HeroObject ThisGameHeroObject { get{ return _thisGameHeroObject; } }

    // 영웅 선택해서 게임 시작 되었는지
    private static bool _isGameStart = false;
    public static bool IsGameStart { get { return _isGameStart; } }

    // 영웅 선택 팝업
    [SerializeField]
    private HeroSelectPopup _heroSelectPopup;
    public HeroSelectPopup HeroSelectPopup { get { return _heroSelectPopup; } }

    // 몬스터 스포너
    [SerializeField]
    private MonsterSpawner _monsterSpawner;
    public MonsterSpawner MonsterSpawner { get { return _monsterSpawner; } }


    // 게임 시작
    public void PlayStart(HeroID selectHeroID)
    {
        // 게임시작 bool 을 true로
        _isGameStart = true;

        // 선택한 영웅을 이번게임의 영웅으로 할당
        _thisGameHeroObject = Managers.Instance.ObjectManager.HeroObjectDict[selectHeroID];

        // 필드에 스폰하고 스탯도 넣어줌
        _thisGameHeroObject = Instantiate(Managers.Instance.ObjectManager.HeroObjectDict[selectHeroID]);
        _thisGameHeroObject.DataSetting();

        // 몬스터도 스폰
        _monsterSpawner.StartMonsterSpawn();

        // 준비 다 되었으니 공격도 시작
        _thisGameHeroObject.AttackStart();
    }
}
