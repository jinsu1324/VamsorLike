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

    // ���� �̹� ���ӿ� ������ ����
    private static HeroObject _thisGameHeroObject;
    public static HeroObject ThisGameHeroObject { get{ return _thisGameHeroObject; } }

    // ���� �����ؼ� ���� ���� �Ǿ�����
    private static bool _isGameStart = false;
    public static bool IsGameStart { get { return _isGameStart; } }

    // ���� ���� �˾�
    [SerializeField]
    private HeroSelectPopup _heroSelectPopup;
    public HeroSelectPopup HeroSelectPopup { get { return _heroSelectPopup; } }

    // ���� ������
    [SerializeField]
    private MonsterSpawner _monsterSpawner;
    public MonsterSpawner MonsterSpawner { get { return _monsterSpawner; } }


    // ���� ����
    public void PlayStart(HeroID selectHeroID)
    {
        // ���ӽ��� bool �� true��
        _isGameStart = true;

        // ������ ������ �̹������� �������� �Ҵ�
        _thisGameHeroObject = Managers.Instance.ObjectManager.HeroObjectDict[selectHeroID];

        // �ʵ忡 �����ϰ� ���ȵ� �־���
        _thisGameHeroObject = Instantiate(Managers.Instance.ObjectManager.HeroObjectDict[selectHeroID]);
        _thisGameHeroObject.DataSetting();

        // ���͵� ����
        _monsterSpawner.StartMonsterSpawn();

        // �غ� �� �Ǿ����� ���ݵ� ����
        _thisGameHeroObject.AttackStart();
    }
}
