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
    private CharacterDataManager _characterDataManager;
    public CharacterDataManager CharacterDataManager { get { return _characterDataManager; } }

    // 캐릭터 리소스 매니저
    [SerializeField]
    private CharacterResourcesManager _characterResourcesManager;
    public CharacterResourcesManager CharacterResourcesManager { get { return _characterResourcesManager; } }

    // 캐릭터 슬롯 매니저
    [SerializeField]
    private CharacterSlotManager _characterSlotManager;
    public CharacterSlotManager CharacterSlotManager { get { return _characterSlotManager; } }

    [Title("Popups", bold: false)]
    // 캐릭터 생성 팝업
    [SerializeField]
    private CharacterMakePopup _characterMakePopup;
    public CharacterMakePopup CharacterMakePopup { get { return _characterMakePopup; } }
}
