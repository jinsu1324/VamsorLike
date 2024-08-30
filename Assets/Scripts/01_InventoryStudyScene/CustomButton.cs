using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{    
    public List<CHANGETYPE> ChangeTypeList { get; set; } = new List<CHANGETYPE>();

    private Action<STATE, CustomButton> _enterAction;
    private Action<STATE, CustomButton> _exitAction;

    private void Start()
    {
        foreach (CHANGETYPE chageType in ChangeTypeList)
        {
            if (chageType == CHANGETYPE.Color)
            {
                _enterAction += CustomButtonMaker.ChangeColor;
                _exitAction += CustomButtonMaker.ChangeColor;
            }
            else if (chageType == CHANGETYPE.Size)
            {
                _enterAction += CustomButtonMaker.ChangeSize;
                _exitAction += CustomButtonMaker.ChangeSize;
            }
        } 
    }
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        _enterAction(STATE.Enter, this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _exitAction(STATE.Exit, this);
    }

    
}
