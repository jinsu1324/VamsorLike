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
        GameObject item = _itemPoolDict[itemID].GetObj();
        item.transform.position = pos;

    }
}
