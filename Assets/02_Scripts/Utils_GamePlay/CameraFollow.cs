using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;  // SmoothDamp에서 사용할 속도 저장 변수

    private void LateUpdate()
    {
        Follow();
    }

    /// <summary>
    /// 카메라 따라가기
    /// </summary>
    private void Follow()
    {
        if (PlaySceneManager.Instance.IsGameStart)
        {
            Vector3 targetPosition = PlaySceneManager.ThisGameHeroObject.transform.position + new Vector3(0, 0, -10);


            // Cinemachine을 사용하면 이렇게 간단하게 카메라 따라가기 가능
            // Late -> Lerp   / FixedUpdate -> SmoothDamp


            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.3f * Time.deltaTime);
        }
    }
}
