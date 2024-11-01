using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingViews : MonoBehaviour
{
    [SerializeField]
    private GameObject _inLoadingView;                // 로딩 들어갈때 보여줄 로딩화면

    [SerializeField]
    private GameObject _outLoadingView;               // 로딩 나올때 보여줄 로딩화면

    /// <summary>
    /// 로딩 들어갈 때
    /// </summary>
    public void InLoadingView_ON()
    {
        _inLoadingView.SetActive(true);
        _outLoadingView.SetActive(false);
    }

    /// <summary>
    /// 로딩 나올 때
    /// </summary>
    public void OutLoadingView_ON()
    {
        _inLoadingView.SetActive(false);
        _outLoadingView.SetActive(true);
    }   
}
