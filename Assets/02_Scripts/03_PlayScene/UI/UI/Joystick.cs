using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

/// <summary>
/// 조이스틱 
/// </summary>
public class Joystick : SerializedMonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private RectTransform _background;               // 조이스틱 배경
    [SerializeField]
    private RectTransform _handle;                   // 조이스틱 핸들

    private Vector2 _inputVector;                    // 조이스틱 입력 벡터
    public float Horizontal => _inputVector.x;       // 조이스틱 입력 벡터 x값(수평 입력 값) 반환 (get 프로퍼티임(return 000))
    public float Vertical => _inputVector.y;         // 조이스틱 입력 벡터 y값(수직 입력 값) 반환 (get 프로퍼티임(return 000))

    public Camera _uiCamera;                         // 사용할 UI 카메라

    /// <summary>
    /// Awake
    /// </summary>
    private void Awake()
    {
        _uiCamera = PlaySceneCanvas.Instance.UICamera;
    }

    /// <summary>
    /// 터치다운 이벤트 처리
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        // 터치시의 스크린 좌표를 로컬좌표로 변환해서 localPoint 에 넣음
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _background.parent as RectTransform,
            eventData.position,
            _uiCamera,
            out localPoint);

        // 조이스틱 백그라운드 이미지의 로컬위치를 변환한 localPoint 위치로 설정
        _background.localPosition = localPoint;

        // 터치 시 즉시 드래그 이벤트 처리
        OnDrag(eventData);
    }

    /// <summary>
    /// 드래그 이벤트 처리
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        // 드래그시의 스크린 좌표를 로컬좌표로 변환해서 localPoint 에 넣음
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _background,
            eventData.position,
            _uiCamera,
            out localPoint);

        // 핸들 배경 빠져나가지 않게
        _handle.localPosition = Vector2.ClampMagnitude(localPoint, _background.sizeDelta.x / 2);

        // 핸들 방향 계산 (1로 고정)
        _inputVector = (_handle.anchoredPosition / (_background.sizeDelta.x / 2)).normalized;
    }

    /// <summary>
    /// 터치 해제 시 처리
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        // 값들 초기화 + 초기 위치로
        _inputVector = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;
        _background.anchoredPosition = new Vector2(0, -600);
    }
}
