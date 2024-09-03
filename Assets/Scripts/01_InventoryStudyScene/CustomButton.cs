using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{    
    public List<ChangeType> ChangeTypeList { get; set; } = new List<ChangeType>();

    private Action<State, CustomButton> _enterAction;
    private Action<State, CustomButton> _exitAction;

    private void Start()
    {
        foreach (ChangeType chageType in ChangeTypeList)
        {
            if (chageType == ChangeType.Color)
            {
                _enterAction += CustomButtonMaker.ChangeColor;
                _exitAction += CustomButtonMaker.ChangeColor;
            }
            else if (chageType == ChangeType.Size)
            {
                _enterAction += CustomButtonMaker.ChangeSize;
                _exitAction += CustomButtonMaker.ChangeSize;
            }
        } 
    }
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        _enterAction(State.Enter, this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _exitAction(State.Exit, this);
    }

    
}
