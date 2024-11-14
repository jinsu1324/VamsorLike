using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

/// <summary>
/// ���̽�ƽ 
/// </summary>
public class Joystick : SerializedMonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private RectTransform _background;               // ���̽�ƽ ���
    [SerializeField]
    private RectTransform _handle;                   // ���̽�ƽ �ڵ�

    private Vector2 _inputVector;                    // ���̽�ƽ �Է� ����
    public float Horizontal => _inputVector.x;       // ���̽�ƽ �Է� ���� x��(���� �Է� ��) ��ȯ (get ������Ƽ��(return 000))
    public float Vertical => _inputVector.y;         // ���̽�ƽ �Է� ���� y��(���� �Է� ��) ��ȯ (get ������Ƽ��(return 000))

    public Camera _uiCamera;                         // ����� UI ī�޶�

    /// <summary>
    /// Awake
    /// </summary>
    private void Awake()
    {
        _uiCamera = PlaySceneCanvas.Instance.UICamera;
    }

    /// <summary>
    /// ��ġ�ٿ� �̺�Ʈ ó��
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        // ��ġ���� ��ũ�� ��ǥ�� ������ǥ�� ��ȯ�ؼ� localPoint �� ����
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _background.parent as RectTransform,
            eventData.position,
            _uiCamera,
            out localPoint);

        // ���̽�ƽ ��׶��� �̹����� ������ġ�� ��ȯ�� localPoint ��ġ�� ����
        _background.localPosition = localPoint;

        // ��ġ �� ��� �巡�� �̺�Ʈ ó��
        OnDrag(eventData);
    }

    /// <summary>
    /// �巡�� �̺�Ʈ ó��
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        // �巡�׽��� ��ũ�� ��ǥ�� ������ǥ�� ��ȯ�ؼ� localPoint �� ����
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _background,
            eventData.position,
            _uiCamera,
            out localPoint);

        // �ڵ� ��� ���������� �ʰ�
        _handle.localPosition = Vector2.ClampMagnitude(localPoint, _background.sizeDelta.x / 2);

        // �ڵ� ���� ��� (1�� ����)
        _inputVector = (_handle.anchoredPosition / (_background.sizeDelta.x / 2)).normalized;
    }

    /// <summary>
    /// ��ġ ���� �� ó��
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        // ���� �ʱ�ȭ + �ʱ� ��ġ��
        _inputVector = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;
        _background.anchoredPosition = new Vector2(0, -600);
    }
}
