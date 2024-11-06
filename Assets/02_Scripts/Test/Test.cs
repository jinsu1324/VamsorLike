using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Trigger Enemy와 충돌했습니다.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Debug.Log("Trigger Obstacler과 충돌했습니다.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Debug.Log("Trigger Item 충돌했습니다.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("FX"))
        {
            Debug.Log("Trigger FX 충돌했습니다.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Hero"))
        {
            Debug.Log("Trigger Hero 충돌했습니다.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("collision Enemy와 충돌했습니다.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Debug.Log("collision Obstacler과 충돌했습니다.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Debug.Log("collision Item 충돌했습니다.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("FX"))
        {
            Debug.Log("collision FX 충돌했습니다.");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Hero"))
        {
            Debug.Log("collision Hero 충돌했습니다.");
        }
    }
}