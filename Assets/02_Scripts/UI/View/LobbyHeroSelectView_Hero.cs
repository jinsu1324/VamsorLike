using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LobbyHeroSelectView_Hero : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private HeroData _baseHeroData;             // 영웅 정보 데이터

    private LobbyVirtualCamera virtualCamera;   // 버추얼 카메라 받아올 변수

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        virtualCamera = FindObjectOfType<LobbyVirtualCamera>();
    }

    /// <summary>
    /// 클릭하면 실행되는 이벤트
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        // 클릭된 오브젝트의 Transform을 가져옴
        Transform clickedTransform = eventData.pointerPress?.transform; 

        // 카메라 스냅핑
        virtualCamera.SnapCameraToCharacter(clickedTransform);

        // 정보 UI 보여주기
    }
}
