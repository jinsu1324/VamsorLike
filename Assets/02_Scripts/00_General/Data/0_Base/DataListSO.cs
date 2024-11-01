using Sirenix.Serialization;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataListSO<T> : ScriptableObject where T : BaseData
{
    public List<T> DataList = new List<T>();            // 데이터 리스트
    
    private Dictionary<string, T> _dataDict;       // Dictionary로 사용할 때 데이터를 저장할 변수


    /// <summary>
    /// 초기화 메서드 - 리스트를 딕셔너리로 변환
    /// </summary>
    public void InitializeDictionary()
    {
        _dataDict = new Dictionary<string, T>();

        // 각 Data의 고유한 키 값을 설정해 Dictionary로 변환
        foreach (var data in DataList)
            _dataDict[data.ID] = data;
    }

    /// <summary>
    /// 딕셔너리처럼 dataDictionary의 데이터를 조회하는 함수
    /// </summary>
    public T GetDataById(string id)
    {
        // 아직 딕셔너리 초기화 안했으면 초기화
        if (_dataDict == null)
            InitializeDictionary();

        // id에 해당하는 데이터가 있으면 반환
        if (_dataDict.TryGetValue(id, out var data))
            return data;

        Debug.LogWarning($"{id} 데이터를 찾을 수 없습니다.");
        return null;
    }

}
