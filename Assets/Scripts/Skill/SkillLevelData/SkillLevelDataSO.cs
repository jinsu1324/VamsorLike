using Sirenix.Serialization;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ų ���� �����͵� ����Ʈ�� �ִ� ��ũ���ͺ� ������Ʈ
/// </summary>
public class SkillLevelDataSO : ScriptableObject
{
    [SerializeReference] // ���� Ÿ���� ����ȭ�� ����
    public List<SkillData_Base> SkillDataList = new List<SkillData_Base>();
        
}
