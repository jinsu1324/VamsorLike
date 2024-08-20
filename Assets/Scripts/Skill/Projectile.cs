using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : SerializedMonoBehaviour
{
    // public Transform _center;    // ��� �߽���
    private float _radius = 2.0f; // ������
    private float _speed = 2.0f;  // �ӵ�

    private float _angle = 0;

    private int _skillAtkStatus;

    public void RotateProjectile(Vector3 pos)
    {
        _angle += _speed * Time.fixedDeltaTime;
        transform.position = pos + new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0) * _radius;
    }

    public void TakeSkillAtkStatus(int atk)
    {
        _skillAtkStatus = atk;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Monster.ToString())
        {
            collision.GetComponent<MonsterObject>().HPMinus(_skillAtkStatus);
        }
    }
}
