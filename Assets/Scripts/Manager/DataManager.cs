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
    public Dictionary<HeroID, HeroData> HeroDataDict { get; set; } = new Dictionary<HeroID, HeroData>();

    // ���� ������ ��ųʸ�
    [SerializeField]
    public Dictionary<MonsterID, MonsterData> MonsterDataDict { get; set; } = new Dictionary<MonsterID, MonsterData>();

    // ��ų ������ ��ųʸ�
    [SerializeField]
    public Dictionary<SkillID, SkillData> SkillDataDict { get; set; } = new Dictionary<SkillID, SkillData>();


    [Title("������ ����Ʈ��", bold: false)]
    // ���������� ����Ʈ
    [SerializeField]
    public LevelDataList LevelDataList { get; set; }
}
