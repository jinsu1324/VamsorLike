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


    // (MonsterID) �� Key������, ('Key : Header', 'Value : MonsterID�� Header�� �´� ��')�� value������ �ϴ� dictionary 
    Dictionary<string, Dictionary<string, string>> _monsterDictionary = new Dictionary<string, Dictionary<string, string>>();



    private void Awake()
    {
        CharacterDataManager.LoadCharacterData();



        // csv ����

        Data_to_IDKeyDictionary(_textAsset, _monsterDictionary);




        Debug.Log(_monsterDictionary["Skeleton"]["ATK"]);


        //for (int i = 0; i < dataDictionaryList.Count; i++)
        //{
        //    // ������ ��ųʸ� ����Ʈ���� ��ųʸ��� �ݺ������� �ϳ����� �޾ƿ�
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
        // ��ü ������ ��ųʸ� ����Ʈ
        List<Dictionary<string, string>> allDataDictionaryList = LoadCSV.CSV_to_Data(textAsset);

        // ���� ID�� key������ �ϴ� ��ųʸ���, �� ���͵��� �����͸� value�� �־���
        for (int i = 0; i < allDataDictionaryList.Count; i++)
        {
            // ��ü ������ ��ųʸ� ����Ʈ�� �ִ� ��ųʸ� 1��
            Dictionary<string, string> dataDictionary = allDataDictionaryList[i];

            // �� ��ųʸ��� �ִ� MonsterID�� Ű������ ���ְ�, value�� �� ��ųʸ��� �־���
            idKeyDictonary[dataDictionary["MonsterID"]] = dataDictionary;
        }
    }
}
