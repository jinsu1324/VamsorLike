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
    public Dictionary<HeroID, HeroData> HeroDataDict { get; set; } = new Dictionary<HeroID, HeroData>();

    // ���� ������ ��ųʸ�
    [SerializeField]
    public Dictionary<MonsterID, MonsterData> MonsterDataDict { get; set; } = new Dictionary<MonsterID, MonsterData>();

    // ��ų ������ ��ųʸ�
    [SerializeField]
    public Dictionary<SkillID, SkillData> SkillDataDict { get; set; } = new Dictionary<SkillID, SkillData>();
}
