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
    public void RefreshUIText(float minute, float second)
    {
        PlayTimeText.text = string.Format("{0:00}:{1:00}", minute, second);
    }
}
