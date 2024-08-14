using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneManager : SerializedMonoBehaviour
{  
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


    // ���� ����
    public static void PlayStart(HeroData SelectedHeroData)
    {       
        // �Ķ���ͷ� �޾ƿ� ���õ� ���������͸�, ���� �̹� ���ӿ� ������ �������� ��������
        HeroID heroID = (HeroID)Enum.Parse(typeof(HeroID), SelectedHeroData.Id);
        _thisGameHeroObject = Managers.Instance.ObjectManager.HeroObjectDict[heroID];

        // �ʵ忡 �����ϰ� ���ȵ� �־���
        _thisGameHeroObject = Instantiate(Managers.Instance.ObjectManager.HeroObjectDict[heroID]);
        _thisGameHeroObject.Spawn();

        // ���ӽ��� bool �� true��
        Managers.Instance.MonsterSpawnManager.IsSpawned = true;

        Debug.Log($"{_thisGameHeroObject.Name}�� ������ �����մϴ�!!!!");
    }
}
