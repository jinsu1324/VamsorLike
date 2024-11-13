using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : SerializedMonoBehaviour
{
    // 아이템 오브젝트 풀 딕셔너리
    [SerializeField]
    private Dictionary<ItemID, ObjectPool> _itemPoolDict = new Dictionary<ItemID, ObjectPool>();

    // 아이템 등장 확률 가중치 딕셔너리
    private Dictionary<ItemID, int> _itenWeightDict = new Dictionary<ItemID, int>
    {   {ItemID.EXP, 100 },
        {ItemID.Gold, 50 },
        {ItemID.Heal, 5 },
        {ItemID.Magnet, 5 },
    };

    // 총 가중치 계산을 위한 변수
    private int _totalWeight;

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        TotalWeightInitialize();
    }

    /// <summary>
    /// 총 가중치 초기화
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
    /// ItemID 에 맞게 풀에서 아이템을 가져오고 생성
    /// </summary>
    public void SpawnItem(ItemID itemID, Vector3 pos)
    {
        GameObject go = _itemPoolDict[itemID].GetObj();
        go.transform.position = pos;

        ItemBase item = go.GetComponent<ItemBase>();
        item.Initialize();
    }

    /// <summary>
    /// 가중치에 따른 랜덤 아이템을 풀에서 가져오고 생성
    /// </summary>
    public void SpawnRandomItem(Vector3 pos)
    {
        ItemID randomItemID = GetRandomItemDrop();
        SpawnItem(randomItemID, pos);
    }

    /// <summary>
    /// 가중치에 따른 랜덤 아이템 ID 반환
    /// </summary>
    private ItemID GetRandomItemDrop()
    {
        // 0부터 총 가중치 사이에서 랜덤한 값을 선택
        int randomValue = Random.Range(0, _totalWeight);
        int cumulativeWeight = 0;

        foreach (var item in _itenWeightDict)
        {
            cumulativeWeight += item.Value;

            // 랜덤하게 선택된 값이 현재 누적 가중치보다 작으면, 해당 아이템을 반환.
            // (ex) 누적 가중치가 60일 때 randomValue가 55라면 해당 아이템을 반환)
            if (randomValue < cumulativeWeight)
            {
                return item.Key;
            }
        }

        // 오류 방지를 위한 기본값
        return ItemID.EXP;
    }

}
