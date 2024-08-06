using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Slot[] slots = new Slot[4];   
    public Slot[] Slots { get { return slots; } set { slots = value; } }
       

    private void Start()
    {
        BindingInventoryItemData();
        PutChildsInSlots();
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager._mousePosInventory = this; 
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager._mousePosInventory = null; 
    }


    private static void BindingInventoryItemData()
    {
        ConsumeInventoryItemData consumeInventoryItemData = new ConsumeInventoryItemData();
        ConsumeInventoryItemData consumeInventoryItemData2 = new ConsumeInventoryItemData();
        EquipInventoryItemData equipInventoryItemData = new EquipInventoryItemData();
        EquipInventoryItemData equipInventoryItemData2 = new EquipInventoryItemData();

        consumeInventoryItemData._itemID = "회복물약 001";
        consumeInventoryItemData2._itemID = "마나물약 004";
        equipInventoryItemData._itemID = "전사장비 001";
        equipInventoryItemData2._itemID = "궁수장비 002";

        InventoryManager._inventoryItemDatas.Add(consumeInventoryItemData);
        InventoryManager._inventoryItemDatas.Add(consumeInventoryItemData2);
        InventoryManager._inventoryItemDatas.Add(equipInventoryItemData);
        InventoryManager._inventoryItemDatas.Add(equipInventoryItemData2);
    }


    // 이 인벤토리 자식에 있는 모든 슬롯들을, slots 배열에 넣고, 인벤토리도 모두 할당
    private void PutChildsInSlots()
    {
        int index = 0;
        foreach (Slot slot in GetComponentsInChildren<Slot>())
        {
            slot._slotInventory = this;
            slot._slotItem = slot.transform.GetComponentInChildren<Item>();
            if (slot._slotItem != null)
            {
                slot._slotItem._itemInventory = this;
                slot._slotItem._itemNameText = slot._slotItem.GetComponentInChildren<TextMeshProUGUI>();
                slot._slotItem._inventoryItemData = 
                    InventoryManager._inventoryItemDatas[Random.Range(0, InventoryManager._inventoryItemDatas.Count)];
                
                Debug.Log($"아이템 이름은 : {slot._slotItem.gameObject.name}");
                slot._slotItem._inventoryItemData.ShowInfo(slot._slotItem._itemNameText);
            }
                
            slots[index] = slot;
            index++;
        }
    }    
}
