using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������Ÿ�� �θ�
public class ProjectileBase : SerializedMonoBehaviour
{   
    // ��ų�� ���ݷ��� �޾ƿ� ����
    protected float _atk;    

    // ��ų�� ���ݷ��� �޾ƿ�
    public void SetAtk(float atk)
    {
        _atk = atk;
    }    
}
