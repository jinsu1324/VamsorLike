using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum STATE
{
    Enter,
    Exit
}

public enum CHANGETYPE
{
    Color,
    Size
}

public static class CustomButtonMaker
{
    public static void ChangeColor(STATE state, CustomButton customButton)
    {
        if (state == STATE.Enter)
        {
            customButton.GetComponent<Image>().color = Color.black;
        }
        else if (state == STATE.Exit)
        {
            customButton.GetComponent<Image>().color = Color.white;
        }        
    }

    public static void ChangeSize(STATE state, CustomButton customButton)
    {
        if (state == STATE.Enter)
        {
            customButton.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        else if (state == STATE.Exit)
        {
            customButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }
}
