using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ӿ� �ʿ��� �����͵�(��ũ���ͺ� ������Ʈ ��) ��ųʸ� �������ִ� �Ŵ���
public class DataManager : SerializedMonoBehaviour
{
    #region �̱���
    private static DataManager _instance;

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

    public static DataManager Instance
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

    [Title("������ ��ųʸ���", bold: false)]
    // ���� ������ ��ųʸ�
    [SerializeField]
    public Dictionary<HEROID, HeroData> HeroDataDict { get; set; } = new Dictionary<HEROID, HeroData>();

    // ���� ������ ��ųʸ�
    [SerializeField]
    public Dictionary<MONSTERID, MonsterData> MonsterDataDict { get; set; } = new Dictionary<MONSTERID, MonsterData>();

    // ��ų ������ ��ųʸ�
    [SerializeField]
    public Dictionary<SKILLID, SkillData> SkillDataDict { get; set; } = new Dictionary<SKILLID, SkillData>();
}
