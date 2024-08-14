using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : SerializedMonoBehaviour
{
    #region 싱글톤
    private static Managers _instance;

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

    public static Managers Instance
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

    [Title("Managers")]
    // 오브젝트 매니저
    [SerializeField]
    private ObjectManager _objectManager;
    public ObjectManager ObjectManager { get { return _objectManager; } }

    // 데이터 매니저
    [SerializeField]
    private DataManager _dataManager;
    public DataManager DataManager { get { return _dataManager; } }

    // 몬스터 스폰 매니저
    [SerializeField]
    private MonsterSpawnManager _monsterSpawnManager;
    public MonsterSpawnManager MonsterSpawnManager { get { return _monsterSpawnManager; } }
}
