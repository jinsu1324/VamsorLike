using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyVirtualCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _defaultCameraPos;                         // 카메라가 기본으로 위치해 있을 위치

    private CinemachineVirtualCamera _vitualCamera;             // Cinemachine 가상 카메라

    private float _baseOrthoSize = 14.0f;                       // 기본 OrthographicSize 값
    private float _targetOrthoSize = 8.0f;                      // 목표로 할 OrthographicSize 값
    private float _smoothTime = 0.3f;                           // OrthographicSize 값을 부드럽게 변경할 시간
    private float _velocity = 0f;                               // OrthographicSize 값을 부드럽게 변경할 때 사용할 속도


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
        StartCoroutine(ZoomIn_OrthoSize());
    }

    /// <summary>
    /// 카메라 기본위치로 이동 + 줌 아웃
    /// </summary>
    public void SanpCamera_To_DefaultPos()
    {
        _vitualCamera.Follow = _defaultCameraPos;
        StartCoroutine(ZoomOut_OrthoSize());
    }

    /// <summary>
    /// 카메라 줌 인
    /// </summary>
    private IEnumerator ZoomIn_OrthoSize()
    {
        while (Mathf.Abs(_vitualCamera.m_Lens.OrthographicSize - _targetOrthoSize) > 0.01f)
        {
            _vitualCamera.m_Lens.OrthographicSize = Mathf.SmoothDamp(
                _vitualCamera.m_Lens.OrthographicSize,
                _targetOrthoSize,
                ref _velocity,
                _smoothTime
            );
            yield return null;
        }

        // 최종 목표 값으로 정확히 설정
        _vitualCamera.m_Lens.OrthographicSize = _targetOrthoSize;
    }

    /// <summary>
    /// 카메라 줌 아웃
    /// </summary>
    private IEnumerator ZoomOut_OrthoSize()
    {
       while (Mathf.Abs(_vitualCamera.m_Lens.OrthographicSize - _baseOrthoSize) > 0.01f)
        {
            _vitualCamera.m_Lens.OrthographicSize = Mathf.SmoothDamp(
                _vitualCamera.m_Lens.OrthographicSize,
                _baseOrthoSize,
                ref _velocity,
                _smoothTime
            );
            yield return null;
        }

        // 최종 목표 값으로 정확히 설정
        _vitualCamera.m_Lens.OrthographicSize = _baseOrthoSize;
    }
}
