using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : SerializedMonoBehaviour
{
    public Transform _center;    // 원운동 중심점
    public float radius = 2.0f; // 반지름
    public float speed = 2.0f;  // 속도

    private float angle = 0;

    public void RotateProjectile(Vector3 pos)
    {
        angle += speed * Time.fixedDeltaTime;
        transform.position = pos + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
    }
}
