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
/// 조이스틱 
/// </summary>
public class Joystick : SerializedMonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private RectTransform _background;       // 조이스틱 배경
    [SerializeField]
    private RectTransform _handle;           // 조이스틱 핸들

    private Vector2 _inputVector;                    // 조이스틱 입력 벡터
    public float Horizontal => _inputVector.x;       // 조이스틱 입력 벡터 x값(수평 입력 값) 반환 (get 프로퍼티임(return 000))
    public float Vertical => _inputVector.y;         // 조이스틱 입력 벡터 y값(수직 입력 값) 반환 (get 프로퍼티임(return 000))


    /// <summary>
    /// 터치다운 이벤트 처리
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        _background.position = eventData.position;

        // 터치 시 즉시 드래그 이벤트 처리
        OnDrag(eventData);
    }

    /// <summary>
    /// 드래그 이벤트 처리
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        // 핸들 배경 빠져나가지 않게
        _handle.position = eventData.position;
        _handle.anchoredPosition = Vector2.ClampMagnitude(_handle.anchoredPosition, _background.sizeDelta.x / 2);

        // 핸들 방향   
        _inputVector = _handle.anchoredPosition - Vector2.zero;
        _inputVector.Normalize();
    }

    /// <summary>
    /// 터치 해제 시 처리
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        // 값들 초기화
        _inputVector = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;
        _background.anchoredPosition = new Vector2(0, -600);
    }
}
