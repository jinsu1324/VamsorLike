using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneCanvas : SerializedMonoBehaviour
{
    #region �̱���_���̵�x
    private static LobbySceneCanvas _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static LobbySceneCanvas Instance
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

    [SerializeField]
    public LobbyTitleUI LobbyTitleUI { get; set; }               // Ÿ��Ʋ UI

    [SerializeField]
    public LobbyHeroStatPopup LobbyHeroStatPopup { get; set; }   // ���� ���� Popup

    [SerializeField]
    public FadeInOutController LoadingViews { get; set; }               // �ε� View
}