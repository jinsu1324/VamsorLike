using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayScene�� �ʿ��� �۷ι��� �͵� ������ : �̹����ӿ� �÷����ϰ��ִ� ���� / ���ӽ��� ���� / ���ӽ��� / �������� �˾� 
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
    public static HeroObject ThisGameHeroObject { get; set; }

    // ���� �����ؼ� ���� ���� �Ǿ�����
    public static bool IsGameStart { get; set; }

    // ���� ���� �˾�
    [SerializeField]
    public HeroSelectPopup HeroSelectPopup { get; set; }

    // ���� ������
    [SerializeField]
    public MonsterSpawner MonsterSpawner { get; set; }


    // ���� ����
    public void PlayStart(HeroID selectHeroID)
    {
        // ���ӽ��� bool �� true��
        IsGameStart = true;

        // ������ ������ �̹������� �������� �Ҵ�
        ThisGameHeroObject = Managers.Instance.ObjectManager.HeroObjectDict[selectHeroID];

        // �ʵ忡 �����ϰ� ���ȵ� �־���
        ThisGameHeroObject = Instantiate(Managers.Instance.ObjectManager.HeroObjectDict[selectHeroID]);
        ThisGameHeroObject.DataSetting();

        // ���͵� ����
        MonsterSpawner.StartMonsterSpawn();

        // �غ� �� �Ǿ����� ���ݵ� ����
        //_thisGameHeroObject.AttackStart();
    }
}
