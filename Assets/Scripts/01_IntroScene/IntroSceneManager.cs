using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IntroSceneManager : SerializedMonoBehaviour
{
    #region �̱���
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
    // ĳ���� ������ �Ŵ���
    [SerializeField]
    private CharacterDataManager _characterDataManager;
    public CharacterDataManager CharacterDataManager { get { return _characterDataManager; } }

    // ĳ���� ���ҽ� �Ŵ���
    [SerializeField]
    private CharacterResourcesManager _characterResourcesManager;
    public CharacterResourcesManager CharacterResourcesManager { get { return _characterResourcesManager; } }

    // ĳ���� ���� �Ŵ���
    [SerializeField]
    private CharacterSlotManager _characterSlotManager;
    public CharacterSlotManager CharacterSlotManager { get { return _characterSlotManager; } }

    [Title("Popups", bold: false)]
    // ĳ���� ���� �˾�
    [SerializeField]
    private CharacterMakePopup _characterMakePopup;
    public CharacterMakePopup CharacterMakePopup { get { return _characterMakePopup; } }
}
