using System.Collections;
using System.Collections.Generic;
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



        List<Dictionary<string, string>> dataDictionaryList = new List<Dictionary<string, string>>();

        // ����� ������(header + 1)�ؼ� ~ ������ ����� for��
        for (int i = headerIndex + 1; i < horizontalSplitArray.Length; i++)
        {
            // ������ ��� ��鵵 ��ǥ�� �������� ���η� �߶���
            string[] verticalSplitArray = horizontalSplitArray[i].Split(",");


            Dictionary<string, string> dataDictionary = new Dictionary<string, string>();


            // ���� ���η� �� �ڸ� ��ü ������ ����ŭ �ݺ�
            for (int k = 0; k < verticalSplitArray.Length; k++)
            {
                // ����� ���� ����� ���η� �߶����͵�
                string header = headerVerticalSplitArray[k];
                string data = verticalSplitArray[k];

                // ��ųʸ��� ���� : Ű = header ��� = data
                dataDictionary[header] = data;

                //Debug.Log("Key : " + header);
                //Debug.Log("Value : " + dataDictionary[header]);
            }


            dataDictionaryList.Add(dataDictionary);
        }

        return dataDictionaryList;
    }
}
