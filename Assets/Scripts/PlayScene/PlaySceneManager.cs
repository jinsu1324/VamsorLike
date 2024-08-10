using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneManager : SerializedMonoBehaviour
{
    #region �̱���
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
    // ���� ������ �Ŵ���
    [SerializeField]
    private MonsterDataManager _monsterDataManager;
    public MonsterDataManager MonsterDataManager { get { return _monsterDataManager; } }

    // ����� ������ �Ŵ���
    [SerializeField]
    private HeroDataManager _heroDataManager;
    public HeroDataManager HeroDataManager { get { return _heroDataManager; } }
}
