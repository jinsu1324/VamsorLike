using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������Ÿ�� �θ�
public class ProjectileBase : SerializedMonoBehaviour
{   
    // ��ų�� ���ݷ��� �޾ƿ� ����
    protected int _skillAtk;    


    // ��ų�� ���ݷ��� �޾ƿ�
    public void TakeSkillAtk(int atk)
    {
        _skillAtk = atk;
    }    
}
