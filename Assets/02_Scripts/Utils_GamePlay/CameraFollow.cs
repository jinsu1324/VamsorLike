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
}
