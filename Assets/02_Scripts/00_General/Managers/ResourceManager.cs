using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SerializedMonoBehaviour
{
    #region ΩÃ±€≈Ê_æ¿¿Ãµø O
    private static ResourceManager _instance;

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

    public static ResourceManager Instance
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

    // øµøı ø¿∫Í¡ß∆ÆµÈ µÒº≈≥ ∏Æ
    [SerializeField]
    public Dictionary<HeroID, HeroObj> HeroObjectDict { get; set; } = new Dictionary<HeroID, HeroObj>();

    // ∏ÛΩ∫≈Õ ø¿∫Í¡ß∆ÆµÈ µÒº≈≥ ∏Æ
    [SerializeField]
    public Dictionary<MonsterID, MonsterObj> MonsterObjectDict { get; set; } = new Dictionary<MonsterID, MonsterObj>();

    // ∫∏Ω∫ ø¿∫Í¡ß∆ÆµÈ µÒº≈≥ ∏Æ
    [SerializeField]
    public Dictionary<BossID, BossObj> BossObjectDict { get; set; } = new Dictionary<BossID, BossObj>();
}