using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;            // Virtual Camera

    /// <summary>
    /// 따라다닐 대상 설정
    /// </summary>
    public void SetFollowTarget(HeroObject heroObject)
    {
        _virtualCamera.Follow = heroObject.transform;
    }






    //private Vector3 velocity = Vector3.zero;  // SmoothDamp에서 사용할 속도 저장 변수

    //private void LateUpdate()
    //{
    //    Follow();
    //}

    ///// <summary>
    ///// 카메라 따라가기
    ///// </summary>
    //private void Follow()
    //{
    //    if (PlaySceneManager.Instance.IsGameStart)
    //    {
    //        Vector3 targetPosition = PlaySceneManager.ThisGameHeroObject.transform.position + new Vector3(0, 0, -10);

    //        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.3f * Time.deltaTime);
    //    }
    //}
}
