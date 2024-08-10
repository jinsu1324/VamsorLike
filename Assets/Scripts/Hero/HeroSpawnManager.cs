using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawnManager : SerializedMonoBehaviour
{
    // 히어로 프리팹들 딕셔너리
    [SerializeField]
    private Dictionary<HeroID, HeroPrefab> _heroPrefabDict = new Dictionary<HeroID, HeroPrefab>();
    public Dictionary<HeroID, HeroPrefab> HeroPrefabDict { get { return _heroPrefabDict; } }


    // 히어로 선택 완료해서 게임 시작
    public void PlayStart(HeroData heroData)
    {
        Debug.Log($"{heroData.Name}로 게임을 시작합니다!!!!");

        HeroID heroID = (HeroID)Enum.Parse(typeof(HeroID), heroData.Id);
        HeroPrefab heroPrefab = Instantiate(_heroPrefabDict[heroID]);
        heroPrefab.Spawn();
    }
}
