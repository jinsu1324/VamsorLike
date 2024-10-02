using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : SerializedMonoBehaviour
{
    #region �̱���
    private static HeroManager _instance;

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

    public static HeroManager Instance
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

    // ���� ������ ��ųʸ�
    [SerializeField]
    public Dictionary<HeroID, HeroData> HeroDataDict { get; set; } = new Dictionary<HeroID, HeroData>();

    // ���� ������Ʈ�� ��ųʸ�
    [SerializeField]
    public Dictionary<HeroID, HeroObject> HeroObjectDict { get; set; } = new Dictionary<HeroID, HeroObject>();

}
