using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class LobbySceneManager : MonoBehaviour
{
    #region 싱글톤_씬이동x
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

    private LobbyVirtualCamera _virtualCamera;   // 버추얼 카메라 받아올 변수
    
    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        _virtualCamera = FindObjectOfType<LobbyVirtualCamera>();
    }

    /// <summary>
    /// 영웅 미선택 기본 상태일때 호출되는 함수
    /// </summary>
    public void Change_DefaultState()
    {
        // 카메라 기본 위치로 이동 + 줌 아웃
        _virtualCamera.SanpCamera_To_DefaultPos();

        // 영웅 스탯팝업 끄기
        LobbySceneCanvas.Instance.LobbyHeroStatPopup.HideAnim_PopupOFF();
        
        // 타이틀 UI 켜기
        LobbySceneCanvas.Instance.LobbyTitleUI.PopupON();
    }

    /// <summary>
    /// 영웅 선택 상태일때 호출되는 함수
    /// </summary>
    public void Change_HeroSelectState(LobbyHero lobbyHero, PointerEventData eventData)
    {
        // 클릭된 오브젝트의 Transform을 가져옴
        Transform clickedTransform = eventData.pointerPress?.transform;

        // 캐릭터 레디 애니메이션 실행 
        lobbyHero.PlayAnimation_Ready();

        // 카메라 캐릭터 위치로 이동 + 줌 인
        _virtualCamera.SnapCamera_To_Character(clickedTransform);

        // 영웅 스탯팝업 켜기
        LobbySceneCanvas.Instance.LobbyHeroStatPopup.ShowAnim_PopupON(lobbyHero);

        // 타이틀 UI 끄기
        LobbySceneCanvas.Instance.LobbyTitleUI.PopupOFF();

    }
}
