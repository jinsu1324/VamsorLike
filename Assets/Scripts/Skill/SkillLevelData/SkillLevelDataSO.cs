using Sirenix.Serialization;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLevelDataSO : ScriptableObject
{
    [SerializeReference] // 참조 타입의 직렬화를 지원
    public List<SkillData_Base> SkillDataList = new List<SkillData_Base>();
        
    

    //// 스킬 레벨에 따른 스킬 데이터 반환
    //public SkillData_Base GetSkillData(int level)
    //{
    //    return SkillDataList[level];
    //}

}
