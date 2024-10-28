using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SerializedMonoBehaviour
{
    [SerializeField]
    private List<ItemBase> _itemList;           // 아이템들 리스트

    /// <summary>
    /// ItemID 에 맞게 아이템을 가져오고 생성
    /// </summary>
    public void SpawnItem(ItemID itemID, Vector3 pos)
    {
        Instantiate(GetItem(itemID), pos, Quaternion.identity);
    }

    /// <summary>
    /// ItemID 에 맞게 아이템을 반환
    /// </summary>
    private ItemBase GetItem(ItemID itemID)
    {
        ItemBase item = _itemList.Find(x => x.Id == itemID);

        if (item == null)
        {
            Debug.Log("ItemManager 에서 아이템을 찾지 못했습니다.");
            return null;
        }

        return item;
    }
}
