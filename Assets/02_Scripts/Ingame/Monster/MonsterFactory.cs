using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : SerializedMonoBehaviour
{
    #region �̱���_���̵�x
    private static MonsterFactory _instance;

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

    public static MonsterFactory Instance
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

    // ���� ������Ʈ Ǯ
    [SerializeField]
    private Dictionary<BossID, ObjectPool> _bossObjectPoolDict = new Dictionary<BossID, ObjectPool>();

    /// <summary>
    /// ���� ������ �����ؼ� ����
    /// </summary>
    public MonsterObject SettingMonster(MonsterID monsterID)
    {
        // ������Ʈ Ǯ���� ���� ������
        GameObject go = _monsterObjectPoolDict[monsterID].GetObj();

        // ���� ������ �ʱ�ȭ (����)
        MonsterObject monster = go.GetComponent<MonsterObject>();
        monster.DataSetting();

        // �� ���͸� ��ȯ
        return monster;
    }

    /// <summary>
    /// ���� ������ �����ؼ� ����
    /// </summary>
    public BossObject SettingMonster(BossID bossID)
    {
        // ������Ʈ Ǯ���� ���� ������
        GameObject go = _bossObjectPoolDict[bossID].GetObj();

        // ���� ������ �ʱ�ȭ (����)
        BossObject boss = go.GetComponent<BossObject>();
        boss.DataSetting();

        // �� ������ ��ȯ
        return boss;
    }
}
