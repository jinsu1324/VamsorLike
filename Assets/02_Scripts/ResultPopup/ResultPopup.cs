using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultPopup : SerializedMonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _killCountText;             // Å³ Ä«¿îÆ®
    [SerializeField]
    private TextMeshProUGUI _playTimeText;              // ÇÃ·¹ÀÌ ½Ã°£
    [SerializeField]
    private TextMeshProUGUI _earnedGoldText;            // È¹µæÇÑ °ñµå


    /// <summary>
    /// ÆË¾÷ ÄÑ±â
    /// </summary>
    public void OpenPopup()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// ÆË¾÷ ´Ý±â
    /// </summary>
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
