using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Test : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;

    private Vector3 direction;

    void Start()
    {
        UpdateDirectionToPlayer();
    }

    private void FixedUpdate()
    {
        // �÷��̾ ���� �̵�
        transform.position += direction * speed * Time.fixedDeltaTime;

        // ���� ������Ʈ
        UpdateDirectionToPlayer();
    }

    private void UpdateDirectionToPlayer()
    {
        direction = (player.position - transform.position).normalized;
    }
}