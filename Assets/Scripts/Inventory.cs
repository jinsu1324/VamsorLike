using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private void Start()
    {
        // 인벤토리 자식들 현재인벤토리로 바인딩
        foreach (Item item in GetComponentsInChildren<Item>())
            item._invenState = this;
    }

    // 마우스가 인벤토리에 들어왔을 때 1회 호출
    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager._invenState = this;
    }

    // 마우스가 인벤토리에서 나갔을 때 1회 호출
    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager._invenState = null;
    }

    
}
