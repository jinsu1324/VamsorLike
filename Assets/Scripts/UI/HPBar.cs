using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : SerializedMonoBehaviour
{
    // HP�ٰ� ����ٴ� �θ� transform
    private Transform _parentTF;


    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(_parentTF.position + new Vector3(0, 0.8f, 0));
    }

    // �θ� ����
    public void SetParent(Transform tf)
    {
        _parentTF = tf;

        // ĵ�������� hp�� ���̾��Ű ��ġ ����
        this.transform.parent = PlaySceneManager.Instance.HpBarParentTransform;
    }

    // HP �������� ������Ʈ
    public void Update_HPSlider(float currentHp, float maxHp)
    {
        GetComponent<Slider>().value = (float)currentHp / (float)maxHp;
    }
}
