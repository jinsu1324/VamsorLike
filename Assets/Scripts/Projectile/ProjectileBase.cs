using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������Ÿ�� �θ�
public class ProjectileBase : SerializedMonoBehaviour
{   
    // ��ų�� ���ݷ��� �޾ƿ� ����
    protected int _atk;    

    // ��ų�� ���ݷ��� �޾ƿ�
    public void SetProjectileAtk(int atk)
    {
        _atk = atk;
    }    
}
