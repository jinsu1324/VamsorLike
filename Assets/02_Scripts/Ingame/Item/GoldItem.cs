using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldItem : MonoBehaviour
{
    // °ñµå È¹µæÇßÀ» ¶§ ¾×¼Ç
    public static event Action OnGetGold;


    // °ñµå¿¡ ´ê¾ÒÀ»¶§
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {
            OnGetGold?.Invoke();

            Destroy(this.gameObject);
        }
    }
}
