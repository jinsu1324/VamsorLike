using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public Slot[] SlotArr { get; set; }
       
    private void Start()
    {
        SlotArr = new Slot[4];

        BindingInventoryItemData();
        PutChildsInSlots();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager.MousePosInventory = this; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.MousePosInventory = null; 
    }

    private static void BindingInventoryItemData()
    {
        ConsumeInventoryItemData consumeInventoryItemData = new ConsumeInventoryItemData();
        ConsumeInventoryItemData consumeInventoryItemData2 = new ConsumeInventoryItemData();
        EquipInventoryItemData equipInventoryItemData = new EquipInventoryItemData();
        EquipInventoryItemData equipInventoryItemData2 = new EquipInventoryItemData();

        consumeInventoryItemData.ItemID = "회복물약 001";
        consumeInventoryItemData2.ItemID = "마나물약 004";
        equipInventoryItemData.ItemID = "전사장비 001";
        equipInventoryItemData2.ItemID = "궁수장비 002";

        InventoryManager.InventoryItemDataList.Add(consumeInventoryItemData);
        InventoryManager.InventoryItemDataList.Add(consumeInventoryItemData2);
        InventoryManager.InventoryItemDataList.Add(equipInventoryItemData);
        InventoryManager.InventoryItemDataList.Add(equipInventoryItemData2);
    }

    // 이 인벤토리 자식에 있는 모든 슬롯들을, slots 배열에 넣고, 인벤토리도 모두 할당
    private void PutChildsInSlots()
    {
        int index = 0;
        foreach (Slot slot in GetComponentsInChildren<Slot>())
        {
            slot.SlotInventory = this;
            slot.SlotItem = slot.transform.GetComponentInChildren<Item>();
            if (slot.SlotItem != null)
            {
                slot.SlotItem.ItemInventory = this;
                slot.SlotItem.ItemNameText = slot.SlotItem.GetComponentInChildren<TextMeshProUGUI>();
                slot.SlotItem.InventoryItemData = 
                    InventoryManager.InventoryItemDataList[Random.Range(0, InventoryManager.InventoryItemDataList.Count)];
                
                Debug.Log($"아이템 이름은 : {slot.SlotItem.gameObject.name}");
                slot.SlotItem.InventoryItemData.ShowInfo(slot.SlotItem.ItemNameText);
            }

            SlotArr[index] = slot;
            index++;
        }
    }    
}
