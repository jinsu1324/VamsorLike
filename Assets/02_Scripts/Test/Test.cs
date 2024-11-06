using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Trigger Enemy�� �浹�߽��ϴ�.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Debug.Log("Trigger Obstacler�� �浹�߽��ϴ�.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Debug.Log("Trigger Item �浹�߽��ϴ�.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("FX"))
        {
            Debug.Log("Trigger FX �浹�߽��ϴ�.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Hero"))
        {
            Debug.Log("Trigger Hero �浹�߽��ϴ�.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("collision Enemy�� �浹�߽��ϴ�.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Debug.Log("collision Obstacler�� �浹�߽��ϴ�.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Debug.Log("collision Item �浹�߽��ϴ�.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("FX"))
        {
            Debug.Log("collision FX �浹�߽��ϴ�.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Hero"))
        {
            Debug.Log("collision Hero �浹�߽��ϴ�.");
        }
    }
}