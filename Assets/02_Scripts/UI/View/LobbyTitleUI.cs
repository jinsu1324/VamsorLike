using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyTitleUI : MonoBehaviour
{
    /// <summary>
    /// UI �ѱ�
    /// </summary>
    public void PopupON()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// UI ����
    /// </summary>
    public void PopupOFF()
    {
        gameObject.SetActive(false);
    }
}
