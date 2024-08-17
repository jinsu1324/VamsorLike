using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : SerializedMonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static SkillManager _instance;

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

    public static SkillManager Instance
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

    public List<Skill> SkillList { get; set; } = new List<Skill>();

    public void AddSkill(Skill skill)
    {
        SkillList.Add(skill);
    }

    public void RemoveSkill(Skill skill)
    {
        SkillList.Remove(skill);
    }
}
