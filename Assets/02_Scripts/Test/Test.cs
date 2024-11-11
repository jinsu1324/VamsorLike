using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Test : MonoBehaviour
{
    public RectTransform goldIconUI; // UI 골드 아이콘 위치
    public float moveDuration = 2.0f; // 이동에 걸리는 시간

    public GameObject itemIcon;
    public GameObject fieldItem;

    public Camera uiCamera;

   
    private void Start()
    {
        MoveToUI();
    }

    public void MoveToUI()
    {
        StartCoroutine(MoveToUIRoutine());
    }

    private IEnumerator MoveToUIRoutine()
    {
        // 1. 필드위에 있는 골드 아이템의 현재 위치를 스크린 좌표로 변환
        Vector2 startScreenPosition = uiCamera.WorldToScreenPoint(fieldItem.transform.position);

        // 2. UI 골드 아이콘의 위치를 스크린 좌표로 변환
        Vector2 endScreenPosition = RectTransformUtility.WorldToScreenPoint(uiCamera, goldIconUI.position);


        // 3. 곡선의 정점으로 사용할 중간 지점 설정 (시작과 끝 사이의 중간에서 위로 높이 올림)
        //Vector2 centerControlPoint = 
        //    (startScreenPosition + endScreenPosition) / 2 
        //    + Vector2.up * Random.Range(-1000f, 1000f); // 높이를 랜덤으로 설정


        Vector2 centerControlPoint =
            (startScreenPosition + endScreenPosition) / 2
            + Vector2.up * Random.Range(-800f, -1000f); // 높이를 랜덤으로 설정

        // 경과 시간을 저장할 변수 초기화
        float elapsed = 0f;

        // 4. moveDuration 동안 아이템을 포물선으로 이동
        while (elapsed < moveDuration)
        {
            // 경과 시간 업데이트
            elapsed += Time.deltaTime;

            // t 는 0에서 1까지 증가하며, 0이면 시작위치, 1이면 목표 위치에 도달
            float value = elapsed / moveDuration;

            // 5. 베지어 곡선 공식으로 현재 위치 계산 (포물선 이동)
            // B(t) = (1-t)*(1-t)*P0   +   2*(1-t)*t*P1   +   t*t*P2      
            Vector2 currentScreenPosition =
                Mathf.Pow((1 - value), 2) * startScreenPosition +
                2 * (1 - value) * value * centerControlPoint +
                Mathf.Pow(value, 2) * endScreenPosition;

            // 5. 현재 스크린 위치를 월드 좌표로 변환하여 골드 아이템 위치를 업데이트
            itemIcon.transform.position = uiCamera.ScreenToWorldPoint(new Vector3(currentScreenPosition.x, currentScreenPosition.y, uiCamera.nearClipPlane));

            // 한 프레임을 대기하여 애니메이션 효과를 지속
            yield return null;
        }

        // 6. 최종 위치를 UI 골드 아이콘으로 고정
        itemIcon.transform.position = uiCamera.ScreenToWorldPoint(new Vector3(endScreenPosition.x, endScreenPosition.y, uiCamera.nearClipPlane));

        Debug.Log("도착!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
}