using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset _textAsset;

    private Dictionary<string, int> HPDataDictionary = new Dictionary<string, int>();


    [SerializeField]
    private CharacterMakePopup _makeNewCharacterPopup;
    public CharacterMakePopup MakeNewCharacterPopup { get { return _makeNewCharacterPopup; } }

    private void Awake()
    {
        CharacterDataManager.LoadCharacterData();



        // csv 관련

        List<Dictionary<string, string>> dataDictionaryList = LoadCSV.CSV_to_Data(_textAsset);

        for (int i = 0; i < dataDictionaryList.Count; i++)
        {
            // 데이터 딕셔너리 리스트에서 딕셔너리를 반복문으로 하나씩만 받아옴
            Dictionary<string, string> dataDictionary = dataDictionaryList[i];

            string ID = dataDictionary["MonsterID"];
            //Debug.Log($"ID : {ID}");

            int HP = int.Parse(dataDictionary["HP"]);
            //Debug.Log($"HP : {HP}");

            //Debug.Log($"ID : {ID} - HP : {HP}");
        }

    }

}
