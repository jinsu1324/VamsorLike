using Sirenix.Serialization;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataListSO<T> : ScriptableObject where T : BaseData
{
    public List<T> DataList = new List<T>();            // ������ ����Ʈ
    
    private Dictionary<string, T> _dataDict;       // Dictionary�� ����� �� �����͸� ������ ����


    /// <summary>
    /// �ʱ�ȭ �޼��� - ����Ʈ�� ��ųʸ��� ��ȯ
    /// </summary>
    public void InitializeDictionary()
    {
        _dataDict = new Dictionary<string, T>();

        // �� Data�� ������ Ű ���� ������ Dictionary�� ��ȯ
        foreach (var data in DataList)
            _dataDict[data.ID] = data;
    }

    /// <summary>
    /// ��ųʸ�ó�� dataDictionary�� �����͸� ��ȸ�ϴ� �Լ�
    /// </summary>
    public T GetDataById(string id)
    {
        // ���� ��ųʸ� �ʱ�ȭ �������� �ʱ�ȭ
        if (_dataDict == null)
            InitializeDictionary();

        // id�� �ش��ϴ� �����Ͱ� ������ ��ȯ
        if (_dataDict.TryGetValue(id, out var data))
            return data;

        Debug.LogWarning($"{id} �����͸� ã�� �� �����ϴ�.");
        return null;
    }

}
