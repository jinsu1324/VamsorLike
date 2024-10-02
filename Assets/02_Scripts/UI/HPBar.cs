using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : SerializedMonoBehaviour
{
    // HP바가 따라다닐 부모 transform
    private Transform _parentTF;


    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(_parentTF.position + new Vector3(0, 0.8f, 0));
    }

    // 부모 설정
    public void SetParent(Transform tf)
    {
        _parentTF = tf;

        // 캔버스에서 hp바 하이어라키 위치 설정
        this.transform.parent = PlaySceneManager.Instance.HpBarParentTransform;
    }

    // HP 게이지바 업데이트
    public void Update_HPSlider(float currentHp, float maxHp)
    {
        GetComponent<Slider>().value = (float)currentHp / (float)maxHp;
    }
}
