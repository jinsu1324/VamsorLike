using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class SampleClass
{
    public int num;

    public void HI()
    {
        Debug.Log("HI!!!!");
    }

    public static void Bye()
    {
        Debug.Log("Bye!!!!!!!!!!!!");
    }
}

public class ReflectionStudy : MonoBehaviour
{
    private void Start()
    {
        SampleClass sampleClass = new SampleClass();
        Type type = typeof(SampleClass);

        MethodInfo methodInfo = type.GetMethod("HI");
        methodInfo.Invoke(sampleClass, null);

        MethodInfo methodInfo2 = type.GetMethod("Bye");
        methodInfo2.Invoke(null, null);

        FieldInfo fieldInfo = type.GetField("num");
        Debug.Log(fieldInfo.FieldType);

        fieldInfo.SetValue(sampleClass, 10);
        Debug.Log(sampleClass.num);

        sampleClass.num = 1000;
        int count = (int)fieldInfo.GetValue(sampleClass);
        Debug.Log(count);

        object obj = fieldInfo.GetValue(sampleClass);
        int count2 = obj.ConvertTo<int>();
        Debug.Log(count2);
    }
}
