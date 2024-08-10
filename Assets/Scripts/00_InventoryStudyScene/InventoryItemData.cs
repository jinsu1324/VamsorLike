using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemData
{
    private string _itemID;
    public string ItemID { get { return _itemID; } set { _itemID = value; } }

    public virtual void ShowInfo(TextMeshProUGUI tmPro)
    {
        tmPro.text = ItemID;        
    }
}

public class ConsumeInventoryItemData : InventoryItemData
{
    public override void ShowInfo(TextMeshProUGUI tmPro)
    {
        base.ShowInfo(tmPro);
    }

    public void HI()
    {
        Debug.Log("HI");
    }
}

public class EquipInventoryItemData : InventoryItemData
{
    public override void ShowInfo(TextMeshProUGUI tmPro)
    {
        base.ShowInfo(tmPro);
    }
}
