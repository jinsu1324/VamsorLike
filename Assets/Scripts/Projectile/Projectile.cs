using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 프로젝타일 부모
public class Projectile : SerializedMonoBehaviour
{   
    // 스킬의 공격력을 받아올 변수
    protected int _skillAtk;    


    // 스킬의 공격력을 받아옴
    public void TakeSkillAtk(int atk)
    {
        _skillAtk = atk;
    }    
}
