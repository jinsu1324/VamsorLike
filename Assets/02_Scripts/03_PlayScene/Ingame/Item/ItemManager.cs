using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<ItemBase> _fieldItemList = new List<ItemBase>();   // 필드에 스폰되어있는 아이템 리스트

    private float _magnetItemSpeed = 10;                            // 자석 아이템 속도

    /// <summary>
    /// 자석 아이템 획득 시 호출
    /// </summary>
    public void ActivateMagnet()
    {
        foreach (ItemBase item in _fieldItemList)
        {
            item.StartMoveItemToHero(_magnetItemSpeed);
        }
    }

    /// <summary>
    /// 필드 아이템 리스트에 추가
    /// </summary>
    public void AddFieldItemList(ItemBase item)
    {
        _fieldItemList.Add(item);
    }

    /// <summary>
    /// 필드 아이템 리스트에서 삭제
    /// </summary>
    public void RemoveFieldItemList(ItemBase item)
    {
        _fieldItemList.Remove(item);
    }
}
