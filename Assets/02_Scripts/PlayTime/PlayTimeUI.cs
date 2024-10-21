using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayTimeUI : SerializedMonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _playTimeText;          // 플레이 타임 UI 텍스트


    /// <summary>
    /// 텍스트 갱신
    /// </summary>
    public void RefreshUIText(float minute, float second)
    {
        _playTimeText.text = string.Format("{0:00}:{1:00}", minute, second);
    }
}
