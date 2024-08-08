using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDataManager : SerializedMonoBehaviour
{
    // ������Ʈ�� ���� ScriptableObject�� ��� �޾ƿͼ� ������ ��ųʸ�
    public Dictionary<MonsterID, MonsterData> _monsterDataDict = new Dictionary<MonsterID, MonsterData>();
}
