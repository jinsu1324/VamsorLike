using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    [SerializeField]
    private GoldInvenUI _goldInvenUI;       // ��� �κ��丮 UI
    [SerializeField]
    private GoldObject _goldObject;         // ��� ������Ʈ

    private int _earnedGold = 0;            // �÷��̿��� ȹ���� ���

    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        _goldInvenUI.RefreshGoldText(_earnedGold);

        GoldObject.OnGetGold += GoldUp;
        MonsterObjectBase.OnMonsterDeath += InstantiateGoldObj;
    }

    /// <summary>
    /// ��� ȹ������ �� ó��
    /// </summary>
    private void GoldUp()
    {
        _earnedGold += 1;
        _goldInvenUI.RefreshGoldText(_earnedGold);
    }

    /// <summary>
    /// �ٴڿ� ��� ����
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
    /// �� ��ȯ�ǰų� ������Ʈ �ı��� �� �̺�Ʈ ����
    /// </summary>
    public void OnDisable()
    {
        GoldObject.OnGetGold -= GoldUp;
        MonsterObjectBase.OnMonsterDeath -= InstantiateGoldObj;
    }
}
