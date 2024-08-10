using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : SerializedMonoBehaviour
{
    // ∏ÛΩ∫≈Õ «¡∏Æ∆’µÈ µÒº≈≥ ∏Æ
    [SerializeField]
    private Dictionary<MonsterID, MonsterPrefab> _monsterPrefabDict = new Dictionary<MonsterID, MonsterPrefab>();
    public Dictionary<MonsterID, MonsterPrefab> MonsterPrefabDict { get { return _monsterPrefabDict; } }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MonsterSpawn(MonsterID.Golem);
        }
    }

    private void MonsterSpawn(MonsterID monsterID)
    {
        MonsterPrefab monsterPrefab = Instantiate(_monsterPrefabDict[monsterID]);
        monsterPrefab.Spawn();
    }

}
