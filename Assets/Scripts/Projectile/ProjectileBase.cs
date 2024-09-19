using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 프로젝타일 부모
public class ProjectileBase : SerializedMonoBehaviour
{   
    // 스킬의 공격력을 받아올 변수
    protected int _atk;    

    // 스킬의 공격력을 받아옴
    public void SetProjectileAtk(int atk)
    {
        _atk = atk;
    }    
}
