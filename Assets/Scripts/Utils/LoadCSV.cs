using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D.IK;

public class LoadCSV
{
    /// <summary>
    /// ����CSV�� ScriptableObject�� �����ϰ� CSV�����Ͱ��鵵 �־���
    /// </summary>
    /// <typeparam name="DataType"> T1�� ������ ��������. ScriptableObject�� ��ӹ޴� Data���� �� </typeparam>
    /// <typeparam name="KeyEnum"> T2�� ������ �������� ID�� �������ִ� Enum�̾���� </typeparam>
    /// <param name="textAsset"> textAsset �� �����͸� �������ִ� CSV���� </param>
    public static void CSV_to_ScriptableObject<DataType, KeyEnum>(TextAsset textAsset) where DataType : ScriptableObject where KeyEnum : Enum
    {
        // csv���� ������ �߶��ֱ�
        string csv = textAsset.text;
        string[] csvRaws = csv.Split(System.Environment.NewLine);

        // header�� ,������ ���� �߶��ֱ�
        int headerIndex = 1;
        string[] headers = csvRaws[headerIndex].Split(',');

        Type dataType = typeof(DataType);
        KeyEnum Id;

        // header�� �������ؼ� ��� �� ����ŭ �ݺ�
        for (int i = headerIndex + 1; i < csvRaws.Length; i++)
        {
            string[] datas = csvRaws[i].Split(',');
            DataType T_Data = null;
       
            for (int k = 0; k < datas.Length; k++)
            {
                string header = headers[k];
                string data = datas[k];

                if (header == "Id")
                {
                    // string �̾��� data�� MonsterKey enum ���� ����ȯ (Header�� ID�� ���� �����ʹ� Orc �̷��� �̸��� ����)
                    Id = (KeyEnum)Enum.Parse(typeof(KeyEnum),data);

                    // ������Ʈ���� ID�� ���ϸ����� �� ���ϵ��� ������
                    string path = $"Assets/Resources/{Id}.asset";
                    ScriptableObject so = AssetDatabase.LoadAssetAtPath<DataType>(path);

                    // �������� ��������, �� ��ο� ������ ������ְ� ����
                    if(so == null)
                    {
                        so = ScriptableObject.CreateInstance<DataType>();

                        AssetDatabase.CreateAsset(so, path);
                        AssetDatabase.SaveAssets();
                    }

                    // ������ or ���θ��� ������ monsterData�� ����ȯ�ϰ� ����
                    T_Data = so as DataType;

                    continue;
                }

                // ���÷��� ����ؼ� monsterData�� ���� ���� (ex) header �� HP �̸� monsterData�� HP������ �����Ҽ�����)
                FieldInfo fieldInfo = dataType.GetField(header);

                if (fieldInfo.FieldType == typeof(int))
                {
                    int data_int = int.Parse(data);
                    fieldInfo.SetValue(T_Data, data_int);
                }
                else if (fieldInfo.FieldType == typeof(float))
                {
                    float data_float = float.Parse(data);
                    fieldInfo.SetValue(T_Data, data_float);
                }
                else if (fieldInfo.FieldType == typeof(string))
                {
                    fieldInfo.SetValue(T_Data, data);
                }
            }
        }
    }
}
