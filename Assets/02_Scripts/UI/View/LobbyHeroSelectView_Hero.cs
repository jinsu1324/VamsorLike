using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LobbyHeroSelectView_Hero : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private HeroData _baseHeroData;             // ���� ���� ������

    private LobbyVirtualCamera virtualCamera;   // ���߾� ī�޶� �޾ƿ� ����

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        virtualCamera = FindObjectOfType<LobbyVirtualCamera>();
    }

    /// <summary>
    /// Ŭ���ϸ� ����Ǵ� �̺�Ʈ
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        // Ŭ���� ������Ʈ�� Transform�� ������
        Transform clickedTransform = eventData.pointerPress?.transform; 

        // ī�޶� ������
        virtualCamera.SnapCameraToCharacter(clickedTransform);

        // ���� UI �����ֱ�
    }
}
