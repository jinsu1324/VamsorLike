using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class LobbySceneManager : MonoBehaviour
{
    #region �̱���_���̵�x
    private static LobbySceneManager _instance;

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

    public static LobbySceneManager Instance
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

    private LobbyVirtualCamera _virtualCamera;   // ���߾� ī�޶� �޾ƿ� ����
    
    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        _virtualCamera = FindObjectOfType<LobbyVirtualCamera>();
    }

    /// <summary>
    /// ���� �̼��� �⺻ �����϶� ȣ��Ǵ� �Լ�
    /// </summary>
    public void Change_DefaultState()
    {
        // ī�޶� �⺻ ��ġ�� �̵� + �� �ƿ�
        _virtualCamera.SanpCamera_To_DefaultPos();

        // ���� �����˾� ����
        LobbySceneCanvas.Instance.LobbyHeroStatPopup.HideAnim_PopupOFF();
        
        // Ÿ��Ʋ UI �ѱ�
        LobbySceneCanvas.Instance.LobbyTitleUI.PopupON();
    }

    /// <summary>
    /// ���� ���� �����϶� ȣ��Ǵ� �Լ�
    /// </summary>
    public void Change_HeroSelectState(LobbyHero lobbyHero, PointerEventData eventData)
    {
        // Ŭ���� ������Ʈ�� Transform�� ������
        Transform clickedTransform = eventData.pointerPress?.transform;

        // ĳ���� ���� �ִϸ��̼� ���� 
        lobbyHero.PlayAnimation_Ready();

        // ī�޶� ĳ���� ��ġ�� �̵� + �� ��
        _virtualCamera.SnapCamera_To_Character(clickedTransform);

        // ���� �����˾� �ѱ�
        LobbySceneCanvas.Instance.LobbyHeroStatPopup.ShowAnim_PopupON(lobbyHero);

        // Ÿ��Ʋ UI ����
        LobbySceneCanvas.Instance.LobbyTitleUI.PopupOFF();

    }
}
