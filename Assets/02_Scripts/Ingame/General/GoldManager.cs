using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    [SerializeField]
    private GoldInvenUI _goldInvenUI;       // 골드 인벤토리 UI
    [SerializeField]
    private GoldObject _goldObject;         // 골드 오브젝트

    private int _earnedGold = 0;            // 플레이에서 획득한 골드

    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        _goldInvenUI.RefreshGoldText(_earnedGold);

        GoldObject.OnGetGold += GoldUp;
        MonsterObjectBase.OnMonsterDeath += InstantiateGoldObj;
    }

    /// <summary>
    /// 골드 획득했을 때 처리
    /// </summary>
    private void GoldUp()
    {
        _earnedGold += 1;
        _goldInvenUI.RefreshGoldText(_earnedGold);
    }

    /// <summary>
    /// 바닥에 골드 생성
    /// </summary>
    private void InstantiateGoldObj(MonsterObjectBase monster)
    {
        int randomGold = Random.Range(1, 4);

        if (randomGold == 1)
            Instantiate(_goldObject, monster.transform.position, Quaternion.identity);
        else 
            return;
    }


    /// <summary>
    /// 씬 전환되거나 오브젝트 파괴될 때 이벤트 제거
    /// </summary>
    public void OnDisable()
    {
        GoldObject.OnGetGold -= GoldUp;
        MonsterObjectBase.OnMonsterDeath -= InstantiateGoldObj;
    }
}
