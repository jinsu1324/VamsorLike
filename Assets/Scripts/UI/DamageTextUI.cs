using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ������ �ؽ�Ʈ UI
/// </summary>
public class DamageTextUI : ObjectPoolObject
{
    [SerializeField] 
    private TextMeshProUGUI _damageText;


    /// <summary>
    /// Init �ؽ�Ʈ�� ���ݷ� �ֱ�
    /// </summary>
    public void Init(float atk)
    {
        // �Ҽ��� ����
        int atkInt = Mathf.FloorToInt(atk);

        // �ؽ�Ʈ ����
        _damageText.text = atkInt.ToString();        
    }

    /// <summary>
    /// �ٽ� ������Ʈ Ǯ�� ����������
    /// </summary>
    public void DamageTextUIDestroy()
    {
        BackTrans();
    }
}
