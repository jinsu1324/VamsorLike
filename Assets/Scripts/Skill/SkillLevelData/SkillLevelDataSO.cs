using Sirenix.Serialization;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLevelDataSO : ScriptableObject
{
    [SerializeReference] // ���� Ÿ���� ����ȭ�� ����
    public List<SkillDataBase> SkillDataList = new List<SkillDataBase>();

}
