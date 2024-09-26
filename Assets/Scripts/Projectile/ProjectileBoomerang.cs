using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBoomerang : ProjectileBase
{
    private float _radius = 2.0f; // ������
    private float _speed = 2.0f;  // �ӵ�
    private float _angle = 0; // ���� ������ ����

    // �θ޶� ���� �ֺ����� ȸ��
    public void AroundBoomerang(Vector3 pos)
    {        
        _angle += _speed * Time.fixedDeltaTime;
        transform.position = pos + new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0) * _radius;
    }

    // ���Ϳ� �浹������ ���ݷ¸�ŭ ���� ü�±��
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Monster.ToString())
        {
            collision.GetComponent<MonsterObject>().HPMinus(_atk);
        }
    }
}
