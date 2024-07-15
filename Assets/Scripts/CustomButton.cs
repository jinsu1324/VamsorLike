using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Action<State, CustomButton> EnterAction;
    public Action<State, CustomButton> ExitAction;

    public List<ChageType> _chageTypeList = new List<ChageType>();

    private void Start()
    {
        foreach (ChageType chageType in _chageTypeList)
        {
            if (chageType == ChageType.Color)
            {
                EnterAction += CustomButtonMaker.ChangeColor;
                ExitAction += CustomButtonMaker.ChangeColor;
            }
            else if (chageType == ChageType.Size)
            {
                EnterAction += CustomButtonMaker.ChangeSize;
                ExitAction += CustomButtonMaker.ChangeSize;
            }
        }       
        
    }
   

    public void OnPointerEnter(PointerEventData eventData)
    {
        EnterAction(State.Enter, this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ExitAction(State.Exit, this);
    }

    
}
