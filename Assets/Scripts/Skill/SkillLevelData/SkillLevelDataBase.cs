using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillLevelDataBase<SkillData> : SkillLevelDataBaseParent where SkillData : SkillDataBase
{
    public List<SkillData> SkillLevelDataList = new List<SkillData>();

}
