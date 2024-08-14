using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ӿ� �ʿ��� �����͵�(��ũ���ͺ� ������Ʈ ��) ��ųʸ� �������ִ� �Ŵ���
public class DataManager : SerializedMonoBehaviour
{
    [Title("������ ��ųʸ���", bold: false)]
    // ���� ������ ��ųʸ�
    [SerializeField]
    private Dictionary<HeroID, HeroData> _heroDataDict = new Dictionary<HeroID, HeroData>();
    public Dictionary<HeroID, HeroData> HeroDataDict { get { return _heroDataDict; } set { _heroDataDict = value; } }

    // ���� ������ ��ųʸ�
    [SerializeField]
    private Dictionary<MonsterID, MonsterData> _monsterDataDict = new Dictionary<MonsterID, MonsterData>();
    public Dictionary<MonsterID, MonsterData> MonsterDataDict { get { return _monsterDataDict; } set { _monsterDataDict = value; } }
}
