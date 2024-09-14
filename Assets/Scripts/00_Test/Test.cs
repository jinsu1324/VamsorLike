using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        //SkillLevelDataBaseParent master = DataManager.Instance.SkillDataDict[SkillID.SlashAttack];

        //if (master is SkillLevelData_SlashAttack skillLevelData_slashAttack)
        //{
        //    Debug.Log(skillLevelData_slashAttack.SkillLevelDataList[0].Atk);
        //}

        //SkillLevelData_SlashAttack skillLevelData_slashAttack = DataManager.Instance.SkillDataDict[SkillID.SlashAttack] as SkillLevelData_SlashAttack;

        //if (skillLevelData_slashAttack != null)
        //{
        //    Debug.Log(skillLevelData_slashAttack.SkillLevelDataList[0].Atk);
        //}




        SkillData_SlashAttack slashAttack = DataManager.Instance.SkillDataDict[SkillID.SlashAttack].SkillDataList[0] as SkillData_SlashAttack;

        Debug.Log(slashAttack.Atk);


        //Debug.Log(DataManager.Instance.SkillDataDict[SkillID.SlashAttack].SkillDataList[0].GetType().GetField("Name"));

        //Debug.Log(DataManager.Instance.SkillDataDict[SkillID.SlashAttack].SkillDataList[0].Name);
        //Debug.Log(skillData_SlashAttack_Lv1.Name);

    }
}
