using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillLevelDataBase : ScriptableObject
{
    public List<SkillDataBase> SkillDataList = new List<SkillDataBase>();
}
