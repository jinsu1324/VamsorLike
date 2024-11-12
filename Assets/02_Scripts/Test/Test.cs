using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Test : MonoBehaviour
{
    public Transform _fieldItem;            // 필드 아이템
    public RectTransform _icon;             // 움직일 아이콘        
    public RectTransform _targetIcon;       // 타겟 아이콘
    public float _moveDuration = 2.0f;      // 이동에 걸리는 시간
    public Camera _uiCamera;                // UI카메라
   
    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        StartMove_IconToTargetIcon(_fieldItem, _icon, _targetIcon, _moveDuration, _uiCamera);
    }

    /// <summary>
    /// 아이콘을 타겟아이콘 위치로 이동하는 코루틴 시작
    /// </summary>
    public void StartMove_IconToTargetIcon(Transform fieldItem, RectTransform icon, RectTransform targetIcon, float moveDuration, Camera uiCamera)
    {
        StartCoroutine(Move_IconToTargetIcon(fieldItem, icon, targetIcon, moveDuration, uiCamera));
    }

    /// <summary>
    /// 아이콘을 타겟아이콘 위치로 이동
    /// </summary>
    private IEnumerator Move_IconToTargetIcon(Transform fieldItem, RectTransform icon, RectTransform targetIcon, float moveDuration, Camera uiCamera)
    {
        // 1. 필드위에 있는 아이템의 현재 위치를 스크린 좌표로 변환
        Vector2 startScreenPosition = uiCamera.WorldToScreenPoint(fieldItem.position);

        // 2. UI 타겟 아이콘의 위치를 스크린 좌표로 변환
        Vector2 endScreenPosition = RectTransformUtility.WorldToScreenPoint(uiCamera, targetIcon.position);

        // 3. 곡선의 정점으로 사용할 중간 지점 설정 (중간에서 랜덤한 높이로 위로 올림)
        Vector2 centerControlPoint =
            (startScreenPosition + endScreenPosition) / 2
            + Vector2.up * Random.Range(-800f, -1000f);

        // 경과 시간을 저장할 변수 초기화
        float time = 0f;

        // 4. moveDuration 동안 아이템을 포물선으로 이동
        while (time < moveDuration)
        {
            // 경과 시간 업데이트
            time += Time.deltaTime;

            // value 는 0에서 1까지 증가하며, 0이면 시작위치, 1이면 목표 위치에 도달
            float value = time / moveDuration;

            // 5. 베지어 곡선 공식으로 현재 위치 계산 (포물선 이동)
            // B(t) = (1-t)*(1-t)*P0   +   2*(1-t)*t*P1   +   t*t*P2      
            Vector2 currentScreenPosition =
                Mathf.Pow((1 - value), 2) * startScreenPosition +
                2 * (1 - value) * value * centerControlPoint +
                Mathf.Pow(value, 2) * endScreenPosition;

            // 6. 현재 스크린 위치를 UI위치로 변환
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                targetIcon.parent as RectTransform,
                currentScreenPosition,
                uiCamera,
                out Vector2 uiPosition);

            // 7. icon의 UI위치 업데이트
            icon.GetComponent<RectTransform>().anchoredPosition = uiPosition;
         
            // 한 프레임을 대기하여 애니메이션 효과를 지속
            yield return null;
        }

        // 8. icon의 최종 위치를 targetIcon 위치로 고정
        icon.GetComponent<RectTransform>().anchoredPosition = targetIcon.anchoredPosition;
    }
}