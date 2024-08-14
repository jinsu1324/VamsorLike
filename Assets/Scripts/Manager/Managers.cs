using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임에 필요한 글로벌하게 가지고있는 매니저들 보유
public class Managers : SerializedMonoBehaviour
{
    #region 싱글톤
    private static Managers _instance;

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

    public static Managers Instance
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

    [Title("매니저들", bold: false)]
    // 오브젝트 매니저
    [SerializeField]
    private ObjectManager _objectManager;
    public ObjectManager ObjectManager { get { return _objectManager; } }

    // 데이터 매니저
    [SerializeField]
    private DataManager _dataManager;
    public DataManager DataManager { get { return _dataManager; } }    
}
