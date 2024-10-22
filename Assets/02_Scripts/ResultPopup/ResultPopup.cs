using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultPopup : SerializedMonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _killCountText;             // ų ī��Ʈ
    [SerializeField]
    private TextMeshProUGUI _playTimeText;              // �÷��� �ð�
    [SerializeField]
    private TextMeshProUGUI _earnedGoldText;            // ȹ���� ���


    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public void OpenPopup()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// �˾� �ݱ�
    /// </summary>
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
