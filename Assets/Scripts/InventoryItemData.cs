using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemData
{
    public string _itemID;

    public virtual string ShowInfo()
    {
        return _itemID;
    }
}


public class ConsumeInventoryItemData : InventoryItemData
{
    public override string ShowInfo()
    {
        return "Consume 아이템ID : " + base.ShowInfo();
    }

    public void HI()
    {
        Debug.Log("HI");
    }
}


public class EquipInventoryItemData : InventoryItemData
{
    public override string ShowInfo()
    {
        return "Equip 아이템ID : " + base.ShowInfo();
    }
}
