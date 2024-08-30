using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임에 필요한 게임오브젝트들(프리팹 등) 딕셔너리 가지고있는 매니저
public class ObjectManager : SerializedMonoBehaviour
{
    #region 싱글톤
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

    [Title("오브젝트 딕셔너리들", bold: false)]
    // 영웅 오브젝트들 딕셔너리
    [SerializeField]
    public Dictionary<HEROID, HeroObject> HeroObjectDict { get; set; } = new Dictionary<HEROID, HeroObject>();


    // 몬스터 오브젝트들 딕셔너리
    [SerializeField]
    public Dictionary<MONSTERID, MonsterObject> MonsterObjectDict { get; set; } = new Dictionary<MONSTERID, MonsterObject>();
}
