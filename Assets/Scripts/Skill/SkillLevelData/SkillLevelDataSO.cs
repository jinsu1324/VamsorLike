using Sirenix.Serialization;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스킬 레벨 데이터들 리스트가 있는 스크립터블 오브젝트
/// </summary>
public class SkillLevelDataSO : ScriptableObject
{
    [SerializeReference] // 참조 타입의 직렬화를 지원
    public List<SkillData_Base> SkillDataList = new List<SkillData_Base>();
        
}
