using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToastMessageUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _messageText;

    public void Initialize(int killCount)
    {
        _messageText.text = $"{killCount}kill ´Þ¼º!";

        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }
}
