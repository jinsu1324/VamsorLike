using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : SerializedMonoBehaviour
{
    #region �̱���
    private static MonsterFactory _instance;

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
    private Dictionary<MONSTERID, ObjectPool> _monsterObjectPoolDict = new Dictionary<MONSTERID, ObjectPool>(); 


    // ���� ������ �����ؼ� ����
    public MonsterObject SettingMonster(MONSTERID monsterID)
    {
        // ������Ʈ Ǯ���� ���� ������
        GameObject go = _monsterObjectPoolDict[monsterID].GetObj();

        // ���� ������ �ʱ�ȭ (����)
        MonsterObject monsterobject = go.GetComponent<MonsterObject>();
        monsterobject.DataSetting();

        // �� ���͸� ��ȯ
        return monsterobject;
    }
}
