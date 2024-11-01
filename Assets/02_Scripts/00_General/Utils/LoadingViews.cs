using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingViews : MonoBehaviour
{
    [SerializeField]
    private GameObject _inLoadingView;                // �ε� ���� ������ �ε�ȭ��

    [SerializeField]
    private GameObject _outLoadingView;               // �ε� ���ö� ������ �ε�ȭ��

    /// <summary>
    /// �ε� �� ��
    /// </summary>
    public void InLoadingView_ON()
    {
        _inLoadingView.SetActive(true);
        _outLoadingView.SetActive(false);
    }

    /// <summary>
    /// �ε� ���� ��
    /// </summary>
    public void OutLoadingView_ON()
    {
        _inLoadingView.SetActive(false);
        _outLoadingView.SetActive(true);
    }   
}
