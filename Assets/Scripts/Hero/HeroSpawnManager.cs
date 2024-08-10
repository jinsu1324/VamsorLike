using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawnManager : SerializedMonoBehaviour
{
    // ����� �����յ� ��ųʸ�
    [SerializeField]
    private Dictionary<HeroID, HeroPrefab> _heroPrefabDict = new Dictionary<HeroID, HeroPrefab>();
    public Dictionary<HeroID, HeroPrefab> HeroPrefabDict { get { return _heroPrefabDict; } }


    // ����� ���� �Ϸ��ؼ� ���� ����
    public void PlayStart(HeroData heroData)
    {
        Debug.Log($"{heroData.Name}�� ������ �����մϴ�!!!!");

        HeroID heroID = (HeroID)Enum.Parse(typeof(HeroID), heroData.Id);
        HeroPrefab heroPrefab = Instantiate(_heroPrefabDict[heroID]);
        heroPrefab.Spawn();
    }
}
