using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SerializedMonoBehaviour
{
    #region �̱���_���̵� O
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

    // ���� ������ ��ųʸ�
    [SerializeField]
    public Dictionary<HeroID, HeroData> HeroDataDict { get; set; } = new Dictionary<HeroID, HeroData>();

    // ���� ������ ��ųʸ�
    [SerializeField]
    public Dictionary<MonsterID, MonsterData> MonsterDataDict { get; set; } = new Dictionary<MonsterID, MonsterData>();

    // ���� ������ ��ųʸ�
    [SerializeField]
    public Dictionary<BossID, BossData> BossDataDict { get; set; } = new Dictionary<BossID, BossData>();

    // ��ų ������ ��ųʸ�
    [SerializeField]
    public Dictionary<SkillID, SkillLevelDataSO> SkillDataDict { get; set; } = new Dictionary<SkillID, SkillLevelDataSO>();

    // ���������� ����Ʈ
    [SerializeField]
    public LevelDatas LevelDatas { get; set; }
}
