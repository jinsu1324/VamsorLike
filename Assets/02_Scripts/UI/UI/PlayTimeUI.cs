using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayTimeUI : SerializedMonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI PlayTimeText { get; set; }          // �÷��� Ÿ�� UI �ؽ�Ʈ


    /// <summary>
    /// �ؽ�Ʈ ����
    /// </summary>
    public void RefreshUIText(string playTime)
    {
        PlayTimeText.text = playTime;
    }
}
