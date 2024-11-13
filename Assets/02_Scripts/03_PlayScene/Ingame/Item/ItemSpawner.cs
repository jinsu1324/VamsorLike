using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : SerializedMonoBehaviour
{
    // ������ ������Ʈ Ǯ ��ųʸ�
    [SerializeField]
    private Dictionary<ItemID, ObjectPool> _itemPoolDict = new Dictionary<ItemID, ObjectPool>();

    // ������ ���� Ȯ�� ����ġ ��ųʸ�
    private Dictionary<ItemID, int> _itenWeightDict = new Dictionary<ItemID, int>
    {   {ItemID.EXP, 100 },
        {ItemID.Gold, 50 },
        {ItemID.Heal, 5 },
        {ItemID.Magnet, 5 },
    };

    // �� ����ġ ����� ���� ����
    private int _totalWeight;

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        TotalWeightInitialize();
    }

    /// <summary>
    /// �� ����ġ �ʱ�ȭ
    /// </summary>
    private void TotalWeightInitialize()
    {
        _totalWeight = 0;
        foreach (int weight in _itenWeightDict.Values)
        {
            _totalWeight += weight;
        }
    }
    
    /// <summary>
    /// ItemID �� �°� Ǯ���� �������� �������� ����
    /// </summary>
    public void SpawnItem(ItemID itemID, Vector3 pos)
    {
        GameObject go = _itemPoolDict[itemID].GetObj();
        go.transform.position = pos;

        ItemBase item = go.GetComponent<ItemBase>();
        item.Initialize();
    }

    /// <summary>
    /// ����ġ�� ���� ���� �������� Ǯ���� �������� ����
    /// </summary>
    public void SpawnRandomItem(Vector3 pos)
    {
        ItemID randomItemID = GetRandomItemDrop();
        SpawnItem(randomItemID, pos);
    }

    /// <summary>
    /// ����ġ�� ���� ���� ������ ID ��ȯ
    /// </summary>
    private ItemID GetRandomItemDrop()
    {
        // 0���� �� ����ġ ���̿��� ������ ���� ����
        int randomValue = Random.Range(0, _totalWeight);
        int cumulativeWeight = 0;

        foreach (var item in _itenWeightDict)
        {
            cumulativeWeight += item.Value;

            // �����ϰ� ���õ� ���� ���� ���� ����ġ���� ������, �ش� �������� ��ȯ.
            // (ex) ���� ����ġ�� 60�� �� randomValue�� 55��� �ش� �������� ��ȯ)
            if (randomValue < cumulativeWeight)
            {
                return item.Key;
            }
        }

        // ���� ������ ���� �⺻��
        return ItemID.EXP;
    }

}
