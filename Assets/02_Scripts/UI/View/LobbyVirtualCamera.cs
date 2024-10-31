using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LobbyVirtualCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _defaultCameraPos;                         // 카메라가 기본으로 위치해 있을 위치

    private CinemachineVirtualCamera _vitualCamera;             // Cinemachine 가상 카메라

    private float _zoomOutOrthoSize = 12.0f;                    // 줌 아웃 값
    private float _zoomInOrthoSize = 8.0f;                      // 줌 인 값
    private float _smoothTime = 0.75f;                          // OrthographicSize 값을 부드럽게 변경할 시간


    /// <summary>
    /// Start 메서드에서 가상 카메라 컴포넌트를 가져옴
    /// </summary>
    private void Start()
    {
        _vitualCamera = GetComponent<CinemachineVirtualCamera>();
        _vitualCamera.Follow = _defaultCameraPos;
    }

    /// <summary>
    /// 카메라 캐릭터 위치로 이동 + 줌 인
    /// </summary>
    public void SnapCamera_To_Character(Transform target)
    {
        _vitualCamera.Follow = target;
        ZoomIn_OrthoSize();
    }

    /// <summary>
    /// 카메라 기본위치로 이동 + 줌 아웃
    /// </summary>
    public void SanpCamera_To_DefaultPos()
    {
        _vitualCamera.Follow = _defaultCameraPos;
        ZoomOut_OrthoSize();
    }

    /// <summary>
    /// 카메라 줌 인
    /// </summary>
    private void ZoomIn_OrthoSize()
    {
        DOTween.To(
            () => _vitualCamera.m_Lens.OrthographicSize,
            x => _vitualCamera.m_Lens.OrthographicSize = x,
            _zoomInOrthoSize,
            _smoothTime)
            .SetEase(Ease.OutSine)
            .OnComplete(() => _vitualCamera.m_Lens.OrthographicSize = _zoomInOrthoSize);
    }

    /// <summary>
    /// 카메라 줌 아웃
    /// </summary>
    private void ZoomOut_OrthoSize()
    {
        DOTween.To(
              () => _vitualCamera.m_Lens.OrthographicSize,
              x => _vitualCamera.m_Lens.OrthographicSize = x,
              _zoomOutOrthoSize,
              _smoothTime)
              .SetEase(Ease.OutSine)
              .OnComplete(() => _vitualCamera.m_Lens.OrthographicSize = _zoomOutOrthoSize);
    }
}
