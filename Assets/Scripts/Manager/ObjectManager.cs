using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : SerializedMonoBehaviour
{
    [Title("ø¿∫Í¡ß∆Æ µÒº≈≥ ∏ÆµÈ", bold: false)]
    // øµøı ø¿∫Í¡ß∆ÆµÈ µÒº≈≥ ∏Æ
    [SerializeField]
    private Dictionary<HeroID, HeroObject> _heroObjectDict = new Dictionary<HeroID, HeroObject>();
    public Dictionary<HeroID, HeroObject> HeroObjectDict { get { return _heroObjectDict; } set { _heroObjectDict = value; } }

    // ∏ÛΩ∫≈Õ ø¿∫Í¡ß∆ÆµÈ µÒº≈≥ ∏Æ
    [SerializeField]
    private Dictionary<MonsterID, MonsterObject> _monsterObjectDict = new Dictionary<MonsterID, MonsterObject>();
    public Dictionary<MonsterID, MonsterObject> MonsterObjectDict { get { return _monsterObjectDict; } set { _monsterObjectDict = value; } }
}
