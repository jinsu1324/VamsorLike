using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyMenuItem
{
    private static bool isToggled = false;

    [MenuItem("Tools/Toggle Function")]
    public static void ToggleFunction()
    {
        isToggled = !isToggled;

    }

    [MenuItem("Tools/Toggle Function", true)]
    public static bool ToggleFunctionValidate()
    {
        Menu.SetChecked("Tools/Toggle Function", isToggled);
        return true;
    }



    [MenuItem("Tools/Execute MyFunction")]
   public static void ExecuteMyFunction()
    {
        MonsterManager monsterManager = GameObject.FindObjectOfType<MonsterManager>();

        //Debug.Log(monsterManager.SpawnDelay);
    }
}
