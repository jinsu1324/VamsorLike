using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������Ÿ�� �θ�
public class Projectile : SerializedMonoBehaviour
{   
    // ��ų�� ���ݷ��� �޾ƿ� ����
    protected int _skillAtk;    


    // ��ų�� ���ݷ��� �޾ƿ�
    public void TakeSkillAtk(int atk)
    {
        _skillAtk = atk;
    }


    // ���Ϳ� �浹������ ���ݷ¸�ŭ ���� ü�±��
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Monster.ToString())
        {
            collision.GetComponent<MonsterObject>().HPMinus(_skillAtk);
        }
    }
}
