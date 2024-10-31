using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyVirtualCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _defaultCameraPos;                         // ī�޶� �⺻���� ��ġ�� ���� ��ġ

    private CinemachineVirtualCamera _vitualCamera;             // Cinemachine ���� ī�޶�

    private float _targetOrthoSize = 10.0f;                        // ��ǥ�� �� OrthographicSize ��
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
    /// Ŭ���� ĳ������ Transform�� �޾ƿ� ī�޶� �ش� ĳ���ͷ� �̵�
    /// </summary>
    public void SnapCameraToCharacter(Transform target)
    {
        _vitualCamera.Follow = target;
        StartCoroutine(ChangeOrthoSize());
    }

    /// <summary>
    /// ī�޶��� OrthographicSize ���� �ε巴�� �������ִ� �ڷ�ƾ
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

        // ���� ��ǥ ������ ��Ȯ�� ����
        _vitualCamera.m_Lens.OrthographicSize = _targetOrthoSize;
    }
}
