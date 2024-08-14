using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SerializedMonoBehaviour
{
    [Title("µ•¿Ã≈Õ µÒº≈≥ ∏ÆµÈ", bold: false)]
    // øµøı µ•¿Ã≈Õ µÒº≈≥ ∏Æ
    [SerializeField]
    private Dictionary<HeroID, HeroData> _heroDataDict = new Dictionary<HeroID, HeroData>();
    public Dictionary<HeroID, HeroData> HeroDataDict { get { return _heroDataDict; } set { _heroDataDict = value; } }

    // ∏ÛΩ∫≈Õ µ•¿Ã≈Õ µÒº≈≥ ∏Æ
    [SerializeField]
    private Dictionary<MonsterID, MonsterData> _monsterDataDict = new Dictionary<MonsterID, MonsterData>();
    public Dictionary<MonsterID, MonsterData> MonsterDataDict { get { return _monsterDataDict; } set { _monsterDataDict = value; } }
}
