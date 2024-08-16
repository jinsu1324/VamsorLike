using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // 내가 이번 게임에 선택한 영웅
    public static HeroObject ThisGameHeroObject { get; set; }

    // 영웅 선택해서 게임 시작 되었는지
    public static bool IsGameStart { get; set; }

    // 영웅 선택 팝업
    [SerializeField]
    public HeroSelectPopup HeroSelectPopup { get; set; }

    // 몬스터 스포너
    [SerializeField]
    public MonsterSpawner MonsterSpawner { get; set; }


    // 게임 시작
    public void PlayStart(HeroID selectHeroID)
    {
        // 게임시작 bool 을 true로
        IsGameStart = true;

        // 선택한 영웅을 이번게임의 영웅으로 할당
        ThisGameHeroObject = Managers.Instance.ObjectManager.HeroObjectDict[selectHeroID];

        // 필드에 스폰하고 스탯도 넣어줌
        ThisGameHeroObject = Instantiate(Managers.Instance.ObjectManager.HeroObjectDict[selectHeroID]);
        ThisGameHeroObject.DataSetting();

        // 몬스터도 스폰
        MonsterSpawner.StartMonsterSpawn();

        // 준비 다 되었으니 공격도 시작
        //_thisGameHeroObject.AttackStart();
    }
}
