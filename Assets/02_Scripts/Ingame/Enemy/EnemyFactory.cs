using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : SerializedMonoBehaviour
{
    #region �̱���_���̵�x
    private static EnemyFactory _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static EnemyFactory Instance
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

    // ���� ������Ʈ Ǯ ��ųʸ�
    [SerializeField]
    private Dictionary<MonsterID, ObjectPool> _monsterObjectPoolDict = new Dictionary<MonsterID, ObjectPool>();

    // ���� ������Ʈ Ǯ ��ųʸ�
    [SerializeField]
    private Dictionary<BossID, ObjectPool> _bossObjectPoolDict = new Dictionary<BossID, ObjectPool>();


    /// <summary>
    /// ���� ������ �����ؼ� ����
    /// </summary>
    public MonsterObj SettingMonster(MonsterID monsterID)
    {
        // ������Ʈ Ǯ���� ���� ������
        GameObject go = _monsterObjectPoolDict[monsterID].GetObj();

        // ���� ������ �ʱ�ȭ (����)
        MonsterObj monster = go.GetComponent<MonsterObj>();
        monster.DataSetting();

        // �� ���͸� ��ȯ
        return monster;
    }

    /// <summary>
    /// ���� ������ �����ؼ� ����
    /// </summary>
    public BossObj SettingMonster(BossID bossID)
    {
        // ������Ʈ Ǯ���� ���� ������
        GameObject go = _bossObjectPoolDict[bossID].GetObj();

        // ���� ������ �ʱ�ȭ (����)
        BossObj boss = go.GetComponent<BossObj>();
        boss.DataSetting();

        // �� ������ ��ȯ
        return boss;
    }
}
