using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SerializedMonoBehaviour
{
    #region ΩÃ±€≈Ê_æ¿¿Ãµø O
    private static DataManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }

            return _instance;
        }
    }
    #endregion

    // øµøı µ•¿Ã≈Õ µÒº≈≥ ∏Æ
    [SerializeField]
    public Dictionary<HeroID, HeroData> HeroDataDict { get; set; } = new Dictionary<HeroID, HeroData>();

    // ∏ÛΩ∫≈Õ µ•¿Ã≈Õ µÒº≈≥ ∏Æ
    [SerializeField]
    public Dictionary<MonsterID, MonsterData> MonsterDataDict { get; set; } = new Dictionary<MonsterID, MonsterData>();

    // ∫∏Ω∫ µ•¿Ã≈Õ µÒº≈≥ ∏Æ
    [SerializeField]
    public Dictionary<BossID, BossData> BossDataDict { get; set; } = new Dictionary<BossID, BossData>();

    // Ω∫≈≥ µ•¿Ã≈Õ µÒº≈≥ ∏Æ
    [SerializeField]
    public Dictionary<SkillID, SkillLevelDataSO> SkillDataDict { get; set; } = new Dictionary<SkillID, SkillLevelDataSO>();

    // ∑π∫ßµ•¿Ã≈Õ ∏ÆΩ∫∆Æ
    [SerializeField]
    public LevelDatas LevelDatas { get; set; }
}
