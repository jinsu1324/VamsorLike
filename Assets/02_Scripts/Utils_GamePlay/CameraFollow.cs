using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;            // Virtual Camera

    /// <summary>
    /// ����ٴ� ��� ����
    /// </summary>
    public void SetFollowTarget(HeroObject heroObject)
    {
        _virtualCamera.Follow = heroObject.transform;
    }






    //private Vector3 velocity = Vector3.zero;  // SmoothDamp���� ����� �ӵ� ���� ����

    //private void LateUpdate()
    //{
    //    Follow();
    //}

    ///// <summary>
    ///// ī�޶� ���󰡱�
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
