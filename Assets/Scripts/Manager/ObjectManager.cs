using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ӿ� �ʿ��� ���ӿ�����Ʈ��(������ ��) ��ųʸ� �������ִ� �Ŵ���
public class ObjectManager : SerializedMonoBehaviour
{
    [Title("������Ʈ ��ųʸ���", bold: false)]
    // ���� ������Ʈ�� ��ųʸ�
    [SerializeField]
    private Dictionary<HeroID, HeroObject> _heroObjectDict = new Dictionary<HeroID, HeroObject>();
    public Dictionary<HeroID, HeroObject> HeroObjectDict { get { return _heroObjectDict; } set { _heroObjectDict = value; } }

    // ���� ������Ʈ�� ��ųʸ�
    [SerializeField]
    private Dictionary<MonsterID, MonsterObject> _monsterObjectDict = new Dictionary<MonsterID, MonsterObject>();
    public Dictionary<MonsterID, MonsterObject> MonsterObjectDict { get { return _monsterObjectDict; } set { _monsterObjectDict = value; } }
}
