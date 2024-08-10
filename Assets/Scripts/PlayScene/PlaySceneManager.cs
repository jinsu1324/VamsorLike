using Sirenix.OdinInspector;
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

    // 히어로 데이터 매니저
    [SerializeField]
    private HeroDataManager _heroDataManager;
    public HeroDataManager HeroDataManager { get { return _heroDataManager; } }

    // 몬스터 스폰 매니저
    [SerializeField]
    private MonsterSpawnManager _monsterSpawnManager;
    public MonsterSpawnManager MonsterSpawnManager { get { return _monsterSpawnManager; } }

    // 히어로 스폰 매니저
    [SerializeField]
    private HeroSpawnManager _heroSpawnManager;
    public HeroSpawnManager HeroSpawnManager { get { return _heroSpawnManager; } }


    [Title("Popups", bold: false)]
    // 히어로 선택 팝업
    [SerializeField]
    private HeroSelectPopup _heroSelectPopup;
    public HeroSelectPopup HeroSelectPopup { get { return _heroSelectPopup; } }
}
