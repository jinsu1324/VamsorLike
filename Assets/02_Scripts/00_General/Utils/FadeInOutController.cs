using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutController : MonoBehaviour
{
    [SerializeField]
    private GameObject _fadeInGO;                // ���̵��� GO

    [SerializeField]
    private GameObject _fadeOutGO;               // ���̵� �ƿ� GO

    /// <summary>
    /// ���̵���
    /// </summary>
    public void FadeIn()
    {
        _fadeInGO.SetActive(true);
        _fadeOutGO.SetActive(false);
    }

    /// <summary>
    /// ���̵�ƿ�
    /// </summary>
    public void FadeOut()
    {
        _fadeInGO.SetActive(false);
        _fadeOutGO.SetActive(true);
    }   
}
