using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupONOFF<T> where T : MonoBehaviour
{
    public static void Popup_ON(T popupGO)
    {
        popupGO.gameObject.SetActive(true);
    }

    public static void Popup_OFF(T popupGO)
    {
        popupGO.gameObject.SetActive(false);
    }
}
