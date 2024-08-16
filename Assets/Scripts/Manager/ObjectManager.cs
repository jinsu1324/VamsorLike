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
    public Dictionary<HeroID, HeroObject> HeroObjectDict { get; set; } = new Dictionary<HeroID, HeroObject>();


    // ���� ������Ʈ�� ��ųʸ�
    [SerializeField]
    public Dictionary<MonsterID, MonsterObject> MonsterObjectDict { get; set; } = new Dictionary<MonsterID, MonsterObject>();
}
