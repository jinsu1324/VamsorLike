using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    Enter,
    Exit
}

public enum ChageType
{
    Color,
    Size
}

public static class CustomButtonMaker
{
    public static void ChangeColor(State state, CustomButton customButton)
    {
        if (state == State.Enter)
        {
            customButton.GetComponent<Image>().color = Color.black;
        }
        else if (state == State.Exit)
        {
            customButton.GetComponent<Image>().color = Color.white;
        }        
    }

    public static void ChangeSize(State state, CustomButton customButton)
    {
        if (state == State.Enter)
        {
            customButton.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
        else if (state == State.Exit)
        {
            customButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }

}
