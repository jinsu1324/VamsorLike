using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : SerializedMonoBehaviour
{
    #region ΩÃ±€≈Ê_æ¿¿Ãµø O
    private static ObjectManager _instance;

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

    public static ObjectManager Instance
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
    public Dictionary<HeroID, HeroObject> HeroObjectDict { get; set; } = new Dictionary<HeroID, HeroObject>();

    // ∏ÛΩ∫≈Õ ø¿∫Í¡ß∆ÆµÈ µÒº≈≥ ∏Æ
    [SerializeField]
    public Dictionary<MonsterID, MonsterObject> MonsterObjectDict { get; set; } = new Dictionary<MonsterID, MonsterObject>();
}
