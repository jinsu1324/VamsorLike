using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SerializedMonoBehaviour
{
    [SerializeField]
    private List<ItemBase> _itemList;           // �����۵� ����Ʈ

    /// <summary>
    /// ItemID �� �°� �������� �������� ����
    /// </summary>
    public void SpawnItem(ItemID itemID, Vector3 pos)
    {
        Instantiate(GetItem(itemID), pos, Quaternion.identity);
    }

    /// <summary>
    /// ItemID �� �°� �������� ��ȯ
    /// </summary>
    private ItemBase GetItem(ItemID itemID)
    {
        ItemBase item = _itemList.Find(x => x.Id == itemID);

        if (item == null)
        {
            Debug.Log("ItemManager ���� �������� ã�� ���߽��ϴ�.");
            return null;
        }

        return item;
    }
}
