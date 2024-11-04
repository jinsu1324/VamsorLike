using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class Test : SerializedMonoBehaviour
{

    #region ΩÃ±€≈Ê_æ¿¿Ãµø O
    private static Test _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static Test Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }

            return _instance;
        }
    }
    #endregion


    private void Start()
    {
        GetSkillData_bySkillIDLevel(SkillID.SlashAttack, 1);
    }


    public void GetSkillData_bySkillIDLevel(SkillID skillID, int level)
    {       

        List<SkillData> matchingData = 
            DataManager.Instance.SkillDatas.GetAllDataByCondition(data => data.ID.Contains(skillID.ToString()));

        SkillData go = matchingData.Find(x => x.Level == level);

        Debug.Log(go.Delay);

    }
}


