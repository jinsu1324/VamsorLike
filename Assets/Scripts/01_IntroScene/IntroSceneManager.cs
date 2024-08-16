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
    public CharacterDataManager CharacterDataManager { get; set; }

    // ĳ���� ���ҽ� �Ŵ���
    [SerializeField]
    public CharacterResourcesManager CharacterResourcesManager { get; set; }

    // ĳ���� ���� �Ŵ���
    [SerializeField]
    public CharacterSlotManager CharacterSlotManager { get; set; }

    [Title("Popups", bold: false)]
    // ĳ���� ���� �˾�
    [SerializeField]
    public CharacterMakePopup CharacterMakePopup { get; set; }
}
