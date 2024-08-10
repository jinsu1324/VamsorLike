using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{    
    private List<ChangeType> _changeTypeList = new List<ChangeType>();
    public List<ChangeType> ChangeTypeList { get { return _changeTypeList; } set { _changeTypeList = value; } }

    private Action<State, CustomButton> _enterAction;
    private Action<State, CustomButton> _exitAction;

    private void Start()
    {
        foreach (ChangeType chageType in _changeTypeList)
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
