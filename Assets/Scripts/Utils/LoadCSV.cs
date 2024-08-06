using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class LoadCSV
{
    public static void CSV_to_MonsterData(TextAsset textAsset)
    {
        string csv = textAsset.text;
        string[] csvRaws = csv.Split(System.Environment.NewLine);

        int headerIndex = 1;
        string[] headers = csvRaws[headerIndex].Split(',');

        Type monsterDataType = typeof(MonsterData);
        string ID = "";

        for (int i = headerIndex + 1; i < csvRaws.Length; i++)
        {
            string[] datas = csvRaws[i].Split(',');
            MonsterData monsterData = new MonsterData();
            //MonsterData monsterData = CreateInstance<MonsterData>();

            for (int k = 0; k < datas.Length; k++)
            {
                string header = headers[k];
                string data = datas[k];

                if (header == "ID")
                {
                    ID = data;
                    continue;
                }

                FieldInfo fieldInfo = monsterDataType.GetField(header);
                if (fieldInfo.FieldType == typeof(int))
                {
                    int data_int = int.Parse(data);
                    fieldInfo.SetValue(monsterData, data_int);
                }
                else if (fieldInfo.FieldType == typeof(float))
                {
                    float data_float = float.Parse(data);
                    fieldInfo.SetValue(monsterData, data_float);
                }
                else if (fieldInfo.FieldType == typeof(string))
                {
                    fieldInfo.SetValue(monsterData, data);
                }
            }

            MonsterDataManager._monsterDataDict[ID] = monsterData;
        }
    }

    public static List<Dictionary<string, string>> CSV_to_Data_Public(TextAsset textAsset)
    {
        string csv = textAsset.text;
        string[] csvRaws = csv.Split(System.Environment.NewLine);

        int headerIndex = 1;
        string[] headers = csvRaws[headerIndex].Split(',');

        List<Dictionary<string, string>> dataDictList = new List<Dictionary<string, string>>();

        for (int i = headerIndex + 1; i < csvRaws.Length; i++)
        {
            string[] datas = csvRaws[i].Split(',');

            Dictionary<string, string> dataDict = new Dictionary<string, string>();

            for (int k = 0; k < datas.Length; k++)
            {
                string header = headers[k];
                string data = datas[k];

                dataDict[header] = data;
            }

            dataDictList.Add(dataDict);
        }

        return dataDictList;
    }


    public static List<Dictionary<string, string>> CSV_to_Data(TextAsset textAsset)
    {
        string csv_string = textAsset.text;      
        string[] horizontalSplitArray = csv_string.Split(System.Environment.NewLine); 
                                                                                     
        int headerIndex = 1;       
        string[] headerVerticalSplitArray = horizontalSplitArray[headerIndex].Split(","); 
        
        List<Dictionary<string, string>> dataDictionaryList = new List<Dictionary<string, string>>();

        Type monsterDataType = typeof(MonsterData);

        
        for (int i = headerIndex + 1; i < horizontalSplitArray.Length; i++)
        {
            
            string[] verticalSplitArray = horizontalSplitArray[i].Split(",");            
            Dictionary<string, string> dataDictionary = new Dictionary<string, string>();

            MonsterData monsterData = new MonsterData();
            string ID = "";          
            
            for (int k = 0; k < verticalSplitArray.Length; k++)
            {
                string header = headerVerticalSplitArray[k];
                string data = verticalSplitArray[k];

                dataDictionary[header] = data;

                if (header == "ID")
                {
                    ID = data;
                    continue;
                }

                FieldInfo fieldInfo = monsterDataType.GetField(header);

                if (fieldInfo.FieldType == typeof(int))
                {
                    int dataIntValue = int.Parse(data);
                    fieldInfo.SetValue(monsterData, dataIntValue);
                }

                if (fieldInfo.FieldType == typeof(string))
                {
                    string dataStringValue = data.ToString();
                    fieldInfo.SetValue(monsterData, dataStringValue);
                }

                if (fieldInfo.FieldType == typeof(float))
                {
                    float dataFloatValue = float.Parse(data);
                    fieldInfo.SetValue(monsterData, dataFloatValue);
                }
            }

            MonsterDataManager._monsterDataDict[ID] = monsterData;

            dataDictionaryList.Add(dataDictionary);
        }

        return dataDictionaryList;
    }
}
