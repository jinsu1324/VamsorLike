using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ӿ� �ʿ��� ���ӿ�����Ʈ��(������ ��) ��ųʸ� �������ִ� �Ŵ���
public class ObjectManager : SerializedMonoBehaviour
{
    #region �̱���
    private static ObjectManager _instance;

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

    public static ObjectManager Instance
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

    [Title("������Ʈ ��ųʸ���", bold: false)]
    // ���� ������Ʈ�� ��ųʸ�
    [SerializeField]
    public Dictionary<HEROID, HeroObject> HeroObjectDict { get; set; } = new Dictionary<HEROID, HeroObject>();


    // ���� ������Ʈ�� ��ųʸ�
    [SerializeField]
    public Dictionary<MONSTERID, MonsterObject> MonsterObjectDict { get; set; } = new Dictionary<MONSTERID, MonsterObject>();
}
