using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutController : MonoBehaviour
{
    [SerializeField]
    private GameObject _fadeInGO;                // 페이드인 GO

    [SerializeField]
    private GameObject _fadeOutGO;               // 페이드 아웃 GO

    /// <summary>
    /// 페이드인
    /// </summary>
    public void FadeIn()
    {
        _fadeInGO.SetActive(true);
        _fadeOutGO.SetActive(false);
    }

    /// <summary>
    /// 페이드아웃
    /// </summary>
    public void FadeOut()
    {
        _fadeInGO.SetActive(false);
        _fadeOutGO.SetActive(true);
    }   
}
