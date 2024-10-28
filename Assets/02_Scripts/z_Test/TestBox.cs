using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBox : MonoBehaviour
{
    public RewardBoxPopup RewardBoxPopup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {
            RewardBoxPopup.Initialize_Popup();
        }
    }
}
