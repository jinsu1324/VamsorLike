using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 데미지 텍스트 UI
/// </summary>
public class DamageTextUI : ObjectPoolObject
{
    [SerializeField] 
    private TextMeshProUGUI _damageText;


    /// <summary>
    /// Init 텍스트에 공격력 넣기
    /// </summary>
    public void Init(float atk)
    {
        // 소수점 버림
        int atkInt = Mathf.FloorToInt(atk);

        // 텍스트 설정
        _damageText.text = atkInt.ToString();        
    }

    /// <summary>
    /// 다시 오브젝트 풀로 돌려보내기
    /// </summary>
    public void DamageTextUIDestroy()
    {
        BackTrans();
    }
}
