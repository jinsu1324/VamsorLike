using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    [SerializeField]
    private GoldInvenUI _goldInvenUI;       // °ñµå ÀÎº¥Åä¸® UI
    [SerializeField]
    private GoldObject _goldObject;         // °ñµå ¿ÀºêÁ§Æ®

    private int _earnedGold = 0;            // ÇÃ·¹ÀÌ¿¡¼­ È¹µæÇÑ °ñµå

    /// <summary>
    /// Start ÇÔ¼ö
    /// </summary>
    private void Start()
    {
        _goldInvenUI.RefreshGoldText(_earnedGold);

        GoldObject.OnGetGold += GoldUp;
        MonsterObject.OnMonsterDeath += InstantiateGoldObj;
    }

    /// <summary>
    /// °ñµå È¹µæÇßÀ» ¶§ Ã³¸®
    /// </summary>
    private void GoldUp()
    {
        _earnedGold += 1;
        _goldInvenUI.RefreshGoldText(_earnedGold);
    }

    /// <summary>
    /// ¹Ù´Ú¿¡ °ñµå »ý¼º
    /// </summary>
    private void InstantiateGoldObj(MonsterObject monsterObject)
    {
        int randomGold = Random.Range(1, 4);

        if (randomGold == 1)
            Instantiate(_goldObject, monsterObject.transform.position, Quaternion.identity);
        else 
            return;
    }
}
