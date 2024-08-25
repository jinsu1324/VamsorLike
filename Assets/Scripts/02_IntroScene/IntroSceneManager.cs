using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IntroSceneManager : SerializedMonoBehaviour
{
    #region 싱글톤
    private static IntroSceneManager _instance;

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

    public static IntroSceneManager Instance
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

    [Title("Managers", bold: false)]
    // 캐릭터 데이터 매니저
    [SerializeField]
    public CharacterDataManager CharacterDataManager { get; set; }

    // 캐릭터 리소스 매니저
    [SerializeField]
    public CharacterResourcesManager CharacterResourcesManager { get; set; }

    // 캐릭터 슬롯 매니저
    [SerializeField]
    public CharacterSlotManager CharacterSlotManager { get; set; }

    [Title("Popups", bold: false)]
    // 캐릭터 생성 팝업
    [SerializeField]
    public CharacterMakePopup CharacterMakePopup { get; set; }
}
