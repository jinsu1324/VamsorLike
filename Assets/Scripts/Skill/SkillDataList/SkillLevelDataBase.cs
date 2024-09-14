using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillLevelDataBase<SkillData> : ScriptableObject, ISkillLevelData where SkillData : SkillDataBase
{
    public List<SkillData> SkillDataList = new List<SkillData>();
}
