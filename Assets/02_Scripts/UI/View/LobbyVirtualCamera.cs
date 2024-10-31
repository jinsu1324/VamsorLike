using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyVirtualCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _defaultCameraPos;                         // 카메라가 기본으로 위치해 있을 위치

    private CinemachineVirtualCamera _vitualCamera;             // Cinemachine 가상 카메라

    private float _targetOrthoSize = 10.0f;                        // 목표로 할 OrthographicSize 값
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
    /// 클릭된 캐릭터의 Transform을 받아와 카메라를 해당 캐릭터로 이동
    /// </summary>
    public void SnapCameraToCharacter(Transform target)
    {
        _vitualCamera.Follow = target;
        StartCoroutine(ChangeOrthoSize());
    }

    /// <summary>
    /// 카메라의 OrthographicSize 값을 부드럽게 변경해주는 코루틴
    /// </summary>
    private IEnumerator ChangeOrthoSize()
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
}
