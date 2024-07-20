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



        // csv ����

        List<Dictionary<string, string>> dataDictionaryList = LoadCSV.CSV_to_Data(_textAsset);

        for (int i = 0; i < dataDictionaryList.Count; i++)
        {
            // ������ ��ųʸ� ����Ʈ���� ��ųʸ��� �ݺ������� �ϳ����� �޾ƿ�
            Dictionary<string, string> dataDictionary = dataDictionaryList[i];

            string ID = dataDictionary["MonsterID"];
            //Debug.Log($"ID : {ID}");

            int HP = int.Parse(dataDictionary["HP"]);
            //Debug.Log($"HP : {HP}");

            //Debug.Log($"ID : {ID} - HP : {HP}");
        }

    }

}
