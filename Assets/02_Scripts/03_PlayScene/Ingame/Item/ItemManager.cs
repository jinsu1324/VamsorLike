using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<ItemBase> _fieldItemList = new List<ItemBase>();   // �ʵ忡 �����Ǿ��ִ� ������ ����Ʈ

    private float _magnetItemSpeed = 10;                            // �ڼ� ������ �ӵ�

    /// <summary>
    /// �ڼ� ������ ȹ�� �� ȣ��
    /// </summary>
    public void ActivateMagnet()
    {
        foreach (ItemBase item in _fieldItemList)
        {
            item.StartMoveItemToHero(_magnetItemSpeed);
        }
    }

    /// <summary>
    /// �ʵ� ������ ����Ʈ�� �߰�
    /// </summary>
    public void AddFieldItemList(ItemBase item)
    {
        _fieldItemList.Add(item);
    }

    /// <summary>
    /// �ʵ� ������ ����Ʈ���� ����
    /// </summary>
    public void RemoveFieldItemList(ItemBase item)
    {
        _fieldItemList.Remove(item);
    }
}
