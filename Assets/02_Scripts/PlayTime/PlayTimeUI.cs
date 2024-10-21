using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayTimeUI : SerializedMonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _playTimeText;          // �÷��� Ÿ�� UI �ؽ�Ʈ


    /// <summary>
    /// �ؽ�Ʈ ����
    /// </summary>
    public void RefreshUIText(float minute, float second)
    {
        _playTimeText.text = string.Format("{0:00}:{1:00}", minute, second);
    }
}
