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
        // 플레이어를 향해 이동
        transform.position += direction * speed * Time.fixedDeltaTime;

        // 방향 업데이트
        UpdateDirectionToPlayer();
    }

    private void UpdateDirectionToPlayer()
    {
        direction = (player.position - transform.position).normalized;
    }
}