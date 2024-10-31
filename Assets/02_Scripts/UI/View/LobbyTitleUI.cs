using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyTitleUI : MonoBehaviour
{
    /// <summary>
    /// UI ÄÑ±â
    /// </summary>
    public void PopupON()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// UI ²ô±â
    /// </summary>
    public void PopupOFF()
    {
        gameObject.SetActive(false);
    }
}
