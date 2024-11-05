using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileController : MonoBehaviour
{
    /// <summary>
    /// 카메라 collider가 타일에서 벗어났을 때 (이동 뒷쪽 타일에서 실행됨)
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 카메라 받아오기
        Camera camera = collision.gameObject.GetComponent<Camera>();
        if (camera == null)
            return;

        // 카메라, 타일 방향 계산
        Vector3 direction = camera.transform.position - transform.position;

        // 콜라이더가 벗어났을 때, 카메라가 그 타일의 어느쪽에 위치하는지 판별
        float dirX = direction.x < 0 ? -1 : 1;
        float dirY = direction.y < 0 ? -1 : 1;

        // 상하좌우 어느쪽인지 판별하고 자신(타일)을 이동
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            transform.Translate(Vector3.right * dirX * 128);
        else
            transform.Translate(Vector3.up * dirY * 128);
    }
}
