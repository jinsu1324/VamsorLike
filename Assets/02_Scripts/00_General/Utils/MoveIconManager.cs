using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveIconManager : ObjectPool
{
    private Camera _mainCamera;                          // 메인카메라
    private Camera _uiCamera;                            // UI카메라

    [Title("General")]
    [SerializeField]
    private float _moveDuration = 2.0f;                  // 이동에 걸리는 시간

    [Title("Gold Move")]
    [SerializeField]
    private RectTransform _targetGoldIcon;               // 타겟 골드아이콘
  
    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _mainCamera = Camera.main;
        _uiCamera = PlaySceneCanvas.Instance.UICamera;
    }

    /// <summary>
    /// 아이콘을 타겟아이콘 위치로 이동하는 코루틴 시작
    /// </summary>
    public void StartMove_IconToTargetIcon(Transform fieldItem)
    {
        StartCoroutine(Move_IconToTargetIcon(fieldItem, _targetGoldIcon, _moveDuration, _uiCamera, _mainCamera));
    }

    /// <summary>
    /// 아이콘을 타겟아이콘 위치로 이동
    /// </summary>
    private IEnumerator Move_IconToTargetIcon(Transform fieldItem, RectTransform targetIcon, float moveDuration, Camera uiCamera, Camera mainCamera)
    {
        // 0. 아이콘을 사용하기 위해 풀에서 가져오기
        GameObject icon = GetObj();

        // 1. 필드위에 있는 아이템의 현재 위치를 Main Camera 스크린 좌표로 변환
        Vector2 startScreenPosition = mainCamera.WorldToScreenPoint(fieldItem.position);

        // 2. startScreenPosition을 UI Camera 기준의 Viewport Point로 변환 후, 다시 UI Camera의 Screen Point로 변환
        Vector3 viewportPosition = mainCamera.ScreenToViewportPoint(startScreenPosition);
        startScreenPosition = uiCamera.ViewportToScreenPoint(viewportPosition);

        // 3. UI 타겟 아이콘의 위치를 UI Camera 기준의 스크린 좌표로 변환
        Vector2 endScreenPosition = RectTransformUtility.WorldToScreenPoint(uiCamera, targetIcon.position);

        // 4. 곡선의 정점으로 사용할 중간 지점 설정 (중간에서 랜덤한 높이로 위로 올림)
        Vector2 centerControlPoint =
            (startScreenPosition + endScreenPosition) / 2
            + Vector2.up * Random.Range(200f, 500f);

        // 경과 시간을 저장할 변수 초기화
        float time = 0f;

        // 5. moveDuration 동안 아이템을 포물선으로 이동
        while (time < moveDuration)
        {
            // 경과 시간 업데이트
            time += Time.deltaTime;

            // value 는 0에서 1까지 증가하며, 0이면 시작위치, 1이면 목표 위치에 도달
            float value = time / moveDuration;

            // 6. 베지어 곡선 공식으로 현재 위치 계산 (포물선 이동)
            // B(t) = (1-t)*(1-t)*P0   +   2*(1-t)*t*P1   +   t*t*P2      
            Vector2 currentScreenPosition =
                Mathf.Pow((1 - value), 2) * startScreenPosition +
                2 * (1 - value) * value * centerControlPoint +
                Mathf.Pow(value, 2) * endScreenPosition;

            // 7.현재 스크린 위치를 UI로컬위치로 변환
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                targetIcon.parent as RectTransform,
                currentScreenPosition,
                uiCamera,
                out Vector2 uiPosition);

            // 8. icon의 UI위치 업데이트
            icon.GetComponent<RectTransform>().anchoredPosition = uiPosition;

            // 한 프레임을 대기하여 애니메이션 효과를 지속
            yield return null;
        }

        // 9. icon의 최종 위치를 targetIcon 위치로 고정
        icon.GetComponent<RectTransform>().anchoredPosition = targetIcon.anchoredPosition;

        // 10. 다 이동했으면 풀로 돌려보내기
        icon.gameObject.GetComponent<ObjectPoolObject>().BackTrans();
    }
}
