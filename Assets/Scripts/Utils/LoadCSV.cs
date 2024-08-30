using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D.IK;



public class LoadCSV
{    
    // 몬스터CSV를 ScriptableObject로 저장하고 CSV데이터값들도 넣어줌   
    public static void CSV_to_Data<DataType, KeyEnum>(TextAsset textAsset, SAVEFOLDERNAME saveFolderName) where DataType : ScriptableObject where KeyEnum : Enum
    {
        // csv파일 행으로 잘라주기
        string csv = textAsset.text;
        string[] csvRaws = csv.Split(System.Environment.NewLine);

        // header만 ,단위로 열로 잘라주기
        int headerIndex = 1;
        string[] headers = csvRaws[headerIndex].Split(',');

        Type dataType = typeof(DataType);
        KeyEnum Id;

        // header를 미포함해서 모든 행 수만큼 반복
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
                    // string 이었던 data를 MonsterKey enum 으로 형변환 (Header가 ID인 곳의 데이터는 Orc 이렇게 이름이 있음)
                    Id = (KeyEnum)Enum.Parse(typeof(KeyEnum),data);

                    // 프로젝트에서 ID를 파일명으로 한 파일들을 가져옴
                    string path = $"Assets/Resources/Data/{saveFolderName}/{saveFolderName}_{Id}.asset";
                    ScriptableObject so = AssetDatabase.LoadAssetAtPath<DataType>(path);

                    // 가져오지 못했으면, 그 경로에 파일을 만들어주고 저장
                    if(so == null)
                    {
                        so = ScriptableObject.CreateInstance<DataType>();

                        AssetDatabase.CreateAsset(so, path);
                        AssetDatabase.SaveAssets();
                    }

                    // 가져온 or 새로만든 파일을 monsterData로 형변환하고 대입
                    T_Data = so as DataType;

                    // Id도 스크립터블 오브젝트에 넣어주기 위해 continue는 제거함
                    //continue;
                }

                // 리플렉션 사용해서 monsterData의 값들 설정 (ex) header 가 HP 이면 monsterData의 HP변수를 접근할수있음)
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
