using Sirenix.Serialization;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLevelDataSO : ScriptableObject
{
    [SerializeReference] // ���� Ÿ���� ����ȭ�� ����
    public List<SkillData_Base> SkillDataList = new List<SkillData_Base>();
        
    

    //// ��ų ������ ���� ��ų ������ ��ȯ
    //public SkillData_Base GetSkillData(int level)
    //{
    //    return SkillDataList[level];
    //}

}
