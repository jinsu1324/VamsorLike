using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class LoadCSV
{
    public static List<Dictionary<string, string>> CSV_to_Data(TextAsset textAsset)
    {
        // �ؽ�Ʈ���� ��ü ��Ʈ�� �޾ƿ�
        string csv_string = textAsset.text;

        // ������ (���η�) �ڸ�
        string[] horizontalSplitArray = csv_string.Split(System.Environment.NewLine);

        // ���������� ��� �ε����� ������ (2��°�ϱ� 1)
        int headerIndex = 1;

        // ��� ���θ� ,�� �������� ���η� �ڸ� (ID, HP, ATK �̷��� ��������?)
        string[] headerVerticalSplitArray = horizontalSplitArray[headerIndex].Split(",");     

        // ������ �߶� ���� ������ ������ ��ųʸ� ����Ʈ
        List<Dictionary<string, string>> dataDictionaryList = new List<Dictionary<string, string>>();


        Type monsterType = typeof(MonsterInfo);


        // ����� ������(header + 1)�ؼ� ~ ������ ����� for��
        for (int i = headerIndex + 1; i < horizontalSplitArray.Length; i++)
        {
            // ������ ��� ��鵵 ��ǥ�� �������� ���η� �߶���
            string[] verticalSplitArray = horizontalSplitArray[i].Split(",");

            // ������ ������ ��ųʸ� ����Ʈ�� ���� ��ųʸ�
            Dictionary<string, string> dataDictionary = new Dictionary<string, string>();


            MonsterInfo monsterInfo = new MonsterInfo();
            string ID = "";


            // ���� ���η� �� �ڸ� ��ü ������ ����ŭ �ݺ�
            for (int k = 0; k < verticalSplitArray.Length; k++)
            {
                // ����� ���� ����� ���η� �߶����͵�
                string header = headerVerticalSplitArray[k];
                string data = verticalSplitArray[k];

                // ��ųʸ��� ���� : Ű = header ��� = data
                dataDictionary[header] = data;



                if (header == "ID")
                {
                    ID = data;
                    continue;
                }

                FieldInfo fieldInfo = monsterType.GetField(header);

                if (fieldInfo.FieldType == typeof(int))
                {
                    int dataIntValue = int.Parse(data);
                    fieldInfo.SetValue(monsterInfo, dataIntValue);
                }

                if (fieldInfo.FieldType == typeof(string))
                {
                    string dataStringValue = data.ToString();
                    fieldInfo.SetValue(monsterInfo, dataStringValue);
                }

                if (fieldInfo.FieldType == typeof(float))
                {
                    float dataFloatValue = float.Parse(data);
                    fieldInfo.SetValue(monsterInfo, dataFloatValue);
                }
            }

            MonsterInfo.monsterInfos[ID] = monsterInfo;


            dataDictionaryList.Add(dataDictionary);
        }

        return dataDictionaryList;
    }
}
