using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset _textAsset;

    [SerializeField]
    private CharacterMakePopup _makeNewCharacterPopup;
    public CharacterMakePopup MakeNewCharacterPopup { get { return _makeNewCharacterPopup; } }


    // (MonsterID) 를 Key값으로, ('Key : Header', 'Value : MonsterID의 Header에 맞는 값')를 value값으로 하는 dictionary 
    Dictionary<string, Dictionary<string, string>> _monsterDictionary = new Dictionary<string, Dictionary<string, string>>();



    private void Awake()
    {
        CharacterDataManager.LoadCharacterData();



        // csv 관련

        Data_to_IDKeyDictionary(_textAsset, _monsterDictionary);




        Debug.Log(_monsterDictionary["Skeleton"]["ATK"]);


        //for (int i = 0; i < dataDictionaryList.Count; i++)
        //{
        //    // 데이터 딕셔너리 리스트에서 딕셔너리를 반복문으로 하나씩만 받아옴
        //    Dictionary<string, string> dataDictionary = dataDictionaryList[i];

        //    string ID = dataDictionary["MonsterID"];
        //    //Debug.Log($"ID : {ID}");

        //    //int HP = int.Parse(dataDictionary["HP"]);
        //    //Debug.Log($"HP : {HP}");

        //    Debug.Log($"ID : {ID}");
        //}

    }


    private void Data_to_IDKeyDictionary(TextAsset textAsset, Dictionary<string, Dictionary<string, string>> idKeyDictonary)
    {
        // 전체 데이터 딕셔너리 리스트
        List<Dictionary<string, string>> allDataDictionaryList = LoadCSV.CSV_to_Data(textAsset);

        // 몬스터 ID를 key값으로 하는 딕셔너리에, 그 몬스터들의 데이터를 value로 넣어줌
        for (int i = 0; i < allDataDictionaryList.Count; i++)
        {
            // 전체 데이터 딕셔너리 리스트에 있는 딕셔너리 1개
            Dictionary<string, string> dataDictionary = allDataDictionaryList[i];

            // 그 딕셔너리에 있는 MonsterID를 키값으로 해주고, value로 그 딕셔너리를 넣어줌
            idKeyDictonary[dataDictionary["MonsterID"]] = dataDictionary;
        }
    }
}
