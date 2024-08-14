using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneManager : SerializedMonoBehaviour
{  
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


    // 게임 시작
    public static void PlayStart(HeroData SelectedHeroData)
    {       
        // 파라미터로 받아온 선택된 영웅데이터를, 내가 이번 게임에 선택한 영웅으로 설정해줌
        HeroID heroID = (HeroID)Enum.Parse(typeof(HeroID), SelectedHeroData.Id);
        _thisGameHeroObject = Managers.Instance.ObjectManager.HeroObjectDict[heroID];

        // 필드에 스폰하고 스탯도 넣어줌
        _thisGameHeroObject = Instantiate(Managers.Instance.ObjectManager.HeroObjectDict[heroID]);
        _thisGameHeroObject.Spawn();

        // 게임시작 bool 을 true로
        Managers.Instance.MonsterSpawnManager.IsSpawned = true;

        Debug.Log($"{_thisGameHeroObject.Name}로 게임을 시작합니다!!!!");
    }
}
