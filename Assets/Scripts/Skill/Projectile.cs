using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : SerializedMonoBehaviour
{
    public Transform _center;    // ��� �߽���
    public float radius = 2.0f; // ������
    public float speed = 2.0f;  // �ӵ�

    private float angle = 0;

    public void RotateProjectile(Vector3 pos)
    {
        angle += speed * Time.fixedDeltaTime;
        transform.position = pos + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
    }
}
