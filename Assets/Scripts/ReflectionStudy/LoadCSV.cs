using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class LoadCSV
{
    public static List<Dictionary<string, string>> CSV_to_Data(TextAsset textAsset)
    {
        // 텍스트에셋 전체 스트링 받아옴
        string csv_string = textAsset.text;

        // 행으로 (가로로) 자름
        string[] horizontalSplitArray = csv_string.Split(System.Environment.NewLine);

        // 위에서부터 헤더 인덱스를 가져옴 (2번째니까 1)
        int headerIndex = 1;

        // 헤더 라인만 ,을 기준으로 세로로 자름 (ID, HP, ATK 이렇게 나오겠지?)
        string[] headerVerticalSplitArray = horizontalSplitArray[headerIndex].Split(",");     

        // 실제로 잘라서 담을 마스터 데이터 딕셔너리 리스트
        List<Dictionary<string, string>> dataDictionaryList = new List<Dictionary<string, string>>();


        Type monsterType = typeof(MonsterInfo);


        // 헤더를 미포함(header + 1)해서 ~ 마지막 행까지 for문
        for (int i = headerIndex + 1; i < horizontalSplitArray.Length; i++)
        {
            // 나머지 모든 행들도 쉼표를 기준으로 세로로 잘라줌
            string[] verticalSplitArray = horizontalSplitArray[i].Split(",");

            // 마스터 데이터 딕셔너리 리스트에 넣을 딕셔너리
            Dictionary<string, string> dataDictionary = new Dictionary<string, string>();


            MonsterInfo monsterInfo = new MonsterInfo();
            string ID = "";


            // 가로 세로로 다 자른 전체 데이터 셀만큼 반복
            for (int k = 0; k < verticalSplitArray.Length; k++)
            {
                // 헤더와 실제 내용들 세로로 잘랐던것들
                string header = headerVerticalSplitArray[k];
                string data = verticalSplitArray[k];

                // 딕셔너리에 저장 : 키 = header 밸류 = data
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
