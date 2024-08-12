using Sirenix.OdinInspector;
using System;
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

    // ���� ������ �Ŵ���
    [SerializeField]
    private HeroDataManager _heroDataManager;
    public HeroDataManager HeroDataManager { get { return _heroDataManager; } }

    // ���� ���� �Ŵ���
    [SerializeField]
    private MonsterSpawnManager _monsterSpawnManager;
    public MonsterSpawnManager MonsterSpawnManager { get { return _monsterSpawnManager; } }

    [Title("Popups", bold: false)]
    // ���� ���� �˾�
    [SerializeField]
    private HeroSelectPopup _heroSelectPopup;
    public HeroSelectPopup HeroSelectPopup { get { return _heroSelectPopup; } }

    [Title("")]
    // ���� �̹� ���ӿ� ������ ����
    private HeroObject _thisGameHeroObject;
    public HeroObject ThisGameHeroObject { get{ return _thisGameHeroObject; } }

    // ���� �����ؼ� ���� ���� �Ǿ�����
    private bool _isGameStart = false;
    public bool IsGameStart { get { return _isGameStart; } }


    // ���� ���� �Ϸ��ؼ� ���� ����
    public void PlayStart(HeroData SelectedHeroData)
    {       
        // �Ķ���ͷ� �޾ƿ� ���õ� ���������͸�, ���� �̹� ���ӿ� ������ ������ �־���
        HeroID heroID = (HeroID)Enum.Parse(typeof(HeroID), SelectedHeroData.Id);
        _thisGameHeroObject = _heroDataManager.HeroObjectDict[heroID];

        // �ʵ忡 �����ϰ� ���ȵ� �־���
        _thisGameHeroObject = Instantiate(_heroDataManager.HeroObjectDict[heroID]);
        _thisGameHeroObject.Spawn();

        // ���ӽ��� bool �� true��
        _monsterSpawnManager.IsSpawned = true;

        Debug.Log($"{_thisGameHeroObject.Name}�� ������ �����մϴ�!!!!");
    }
}
