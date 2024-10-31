using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyVirtualCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _defaultCameraPos;                         // ī�޶� �⺻���� ��ġ�� ���� ��ġ

    private CinemachineVirtualCamera _vitualCamera;             // Cinemachine ���� ī�޶�

    private float _baseOrthoSize = 14.0f;                       // �⺻ OrthographicSize ��
    private float _targetOrthoSize = 8.0f;                      // ��ǥ�� �� OrthographicSize ��
    private float _smoothTime = 0.3f;                           // OrthographicSize ���� �ε巴�� ������ �ð�
    private float _velocity = 0f;                               // OrthographicSize ���� �ε巴�� ������ �� ����� �ӵ�


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
        StartCoroutine(ZoomIn_OrthoSize());
    }

    /// <summary>
    /// ī�޶� �⺻��ġ�� �̵� + �� �ƿ�
    /// </summary>
    public void SanpCamera_To_DefaultPos()
    {
        _vitualCamera.Follow = _defaultCameraPos;
        StartCoroutine(ZoomOut_OrthoSize());
    }

    /// <summary>
    /// ī�޶� �� ��
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

        // ���� ��ǥ ������ ��Ȯ�� ����
        _vitualCamera.m_Lens.OrthographicSize = _targetOrthoSize;
    }

    /// <summary>
    /// ī�޶� �� �ƿ�
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

        // ���� ��ǥ ������ ��Ȯ�� ����
        _vitualCamera.m_Lens.OrthographicSize = _baseOrthoSize;
    }
}
