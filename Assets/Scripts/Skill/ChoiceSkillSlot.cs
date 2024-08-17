using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoiceSkillSlot : SerializedMonoBehaviour, IPointerClickHandler
{
    
    public Skill Skill { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        SkillManager.Instance.AddSkill(Skill);
    }

}
