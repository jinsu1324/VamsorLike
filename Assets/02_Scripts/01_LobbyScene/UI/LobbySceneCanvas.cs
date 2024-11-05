using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneCanvas : SerializedMonoBehaviour
{
    #region ΩÃ±€≈Ê_æ¿¿Ãµøx
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
    public LobbyTitleUI LobbyTitleUI { get; set; }               // ≈∏¿Ã∆≤ UI

    [SerializeField]
    public LobbyHeroStatPopup LobbyHeroStatPopup { get; set; }   // øµøı Ω∫≈» Popup

    [SerializeField]
    public FadeInOutController LoadingViews { get; set; }               // ∑Œµ˘ View
}