using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayTimeUI : SerializedMonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI PlayTimeText { get; set; }          // 플레이 타임 UI 텍스트


    /// <summary>
    /// 텍스트 갱신
    /// </summary>
    public void RefreshUIText(string playTime)
    {
        PlayTimeText.text = playTime;
    }
}
