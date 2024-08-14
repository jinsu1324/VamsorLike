using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임에 필요한 게임오브젝트들(프리팹 등) 딕셔너리 가지고있는 매니저
public class ObjectManager : SerializedMonoBehaviour
{
    [Title("오브젝트 딕셔너리들", bold: false)]
    // 영웅 오브젝트들 딕셔너리
    [SerializeField]
    private Dictionary<HeroID, HeroObject> _heroObjectDict = new Dictionary<HeroID, HeroObject>();
    public Dictionary<HeroID, HeroObject> HeroObjectDict { get { return _heroObjectDict; } set { _heroObjectDict = value; } }

    // 몬스터 오브젝트들 딕셔너리
    [SerializeField]
    private Dictionary<MonsterID, MonsterObject> _monsterObjectDict = new Dictionary<MonsterID, MonsterObject>();
    public Dictionary<MonsterID, MonsterObject> MonsterObjectDict { get { return _monsterObjectDict; } set { _monsterObjectDict = value; } }
}
