using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : SerializedMonoBehaviour
{
    // 아이템 오브젝트 풀 딕셔너리
    [SerializeField]
    private Dictionary<ItemID, ObjectPool> _itemPoolDict = new Dictionary<ItemID, ObjectPool>();

    /// <summary>
    /// ItemID 에 맞게 풀에서 아이템을 가져오고 생성
    /// </summary>
    public void SpawnItem(ItemID itemID, Vector3 pos)
    {
        GameObject go = _itemPoolDict[itemID].GetObj();
        go.transform.position = pos;

        ItemBase item = go.GetComponent<ItemBase>();
        item.Initialized();
    }
}
