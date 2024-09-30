using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

/// <summary>
/// ���̽�ƽ 
/// </summary>
public class Joystick : SerializedMonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private RectTransform _background;       // ���̽�ƽ ���
    [SerializeField]
    private RectTransform _handle;           // ���̽�ƽ �ڵ�

    private Vector2 _inputVector;                    // ���̽�ƽ �Է� ����
    public float Horizontal => _inputVector.x;       // ���̽�ƽ �Է� ���� x��(���� �Է� ��) ��ȯ (get ������Ƽ��(return 000))
    public float Vertical => _inputVector.y;         // ���̽�ƽ �Է� ���� y��(���� �Է� ��) ��ȯ (get ������Ƽ��(return 000))


    /// <summary>
    /// ��ġ�ٿ� �̺�Ʈ ó��
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        _background.position = eventData.position;

        // ��ġ �� ��� �巡�� �̺�Ʈ ó��
        OnDrag(eventData);
    }

    /// <summary>
    /// �巡�� �̺�Ʈ ó��
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        // �ڵ� ��� ���������� �ʰ�
        _handle.position = eventData.position;
        _handle.anchoredPosition = Vector2.ClampMagnitude(_handle.anchoredPosition, _background.sizeDelta.x / 2);

        // �ڵ� ����   
        _inputVector = _handle.anchoredPosition - Vector2.zero;
        _inputVector.Normalize();
    }

    /// <summary>
    /// ��ġ ���� �� ó��
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        // ���� �ʱ�ȭ
        _inputVector = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;
        _background.anchoredPosition = new Vector2(0, -600);
    }
}
