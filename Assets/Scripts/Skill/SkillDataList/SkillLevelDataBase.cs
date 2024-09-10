using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillLevelDataBase<SkillData> : ScriptableObject
{
    public List<SkillData> SkillDataList = new List<SkillData>();
}
