using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임에 필요한 데이터들(스크립터블 오브젝트 등) 딕셔너리 가지고있는 매니저
public class DataManager : SerializedMonoBehaviour
{
    [Title("데이터 딕셔너리들", bold: false)]
    // 영웅 데이터 딕셔너리
    [SerializeField]
    private Dictionary<HeroID, HeroData> _heroDataDict = new Dictionary<HeroID, HeroData>();
    public Dictionary<HeroID, HeroData> HeroDataDict { get { return _heroDataDict; } set { _heroDataDict = value; } }

    // 몬스터 데이터 딕셔너리
    [SerializeField]
    private Dictionary<MonsterID, MonsterData> _monsterDataDict = new Dictionary<MonsterID, MonsterData>();
    public Dictionary<MonsterID, MonsterData> MonsterDataDict { get { return _monsterDataDict; } set { _monsterDataDict = value; } }
}
