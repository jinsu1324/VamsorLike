using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LobbyVirtualCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _defaultCameraPos;                         // ī�޶� �⺻���� ��ġ�� ���� ��ġ

    private CinemachineVirtualCamera _vitualCamera;             // Cinemachine ���� ī�޶�

    private float _zoomOutOrthoSize = 12.0f;                    // �� �ƿ� ��
    private float _zoomInOrthoSize = 8.0f;                      // �� �� ��
    private float _smoothTime = 0.75f;                          // OrthographicSize ���� �ε巴�� ������ �ð�


    /// <summary>
    /// Start �޼��忡�� ���� ī�޶� ������Ʈ�� ������
    /// </summary>
    private void Start()
    {
        _vitualCamera = GetComponent<CinemachineVirtualCamera>();
        _vitualCamera.Follow = _defaultCameraPos;
    }

    /// <summary>
    /// ī�޶� ĳ���� ��ġ�� �̵� + �� ��
    /// </summary>
    public void SnapCamera_To_Character(Transform target)
    {
        _vitualCamera.Follow = target;
        ZoomIn_OrthoSize();
    }

    /// <summary>
    /// ī�޶� �⺻��ġ�� �̵� + �� �ƿ�
    /// </summary>
    public void SanpCamera_To_DefaultPos()
    {
        _vitualCamera.Follow = _defaultCameraPos;
        ZoomOut_OrthoSize();
    }

    /// <summary>
    /// ī�޶� �� ��
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
    /// ī�޶� �� �ƿ�
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
