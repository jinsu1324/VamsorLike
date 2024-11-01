using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : SerializedMonoBehaviour
{
    // ������ ������Ʈ Ǯ ��ųʸ�
    [SerializeField]
    private Dictionary<ItemID, ObjectPool> _itemPoolDict = new Dictionary<ItemID, ObjectPool>();

    /// <summary>
    /// ItemID �� �°� Ǯ���� �������� �������� ����
    /// </summary>
    public void SpawnItem(ItemID itemID, Vector3 pos)
    {
        GameObject go = _itemPoolDict[itemID].GetObj();
        go.transform.position = pos;

        ItemBase item = go.GetComponent<ItemBase>();
        item.Initialized();
    }
}
