using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldInvenUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _goldText;          // »πµÊ«— ∞ÒµÂ ≈ÿΩ∫∆Æ

    /// <summary>
    /// ∞ÒµÂ ≈ÿΩ∫∆Æ ªı∑Œ∞Ìƒß
    /// </summary>
    public void RefreshGoldText(int earnedGold)
    {
        _goldText.text = earnedGold.ToString();
    }
}
