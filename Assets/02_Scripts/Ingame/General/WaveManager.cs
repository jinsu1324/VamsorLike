using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{    
    [SerializeField]
    private WaveDatas _waveDatas;                               // 웨이브 데이터
    
    private int _curWaveIndex = 0;                              // 현재 웨이브 인덱스
    private TimeSpan _elapsedTime = TimeSpan.Zero;              // 경과시간

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        EmptyCheck_WaveDatas();
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        Check_WaveTime();
    }

    /// <summary>
    /// 웨이브 데이터 유무 체크
    /// </summary>
    private void EmptyCheck_WaveDatas()
    {
        if (_waveDatas == null || _waveDatas.waveDataArr.Length == 0)
        {
            Debug.LogError("WaveDatas 가 설정되지 않았거나 데이터가 비어있습니다!");
            enabled = false; // 데이터를 불러오지 못하면 스크립트 비활성화
        }
    }   

    /// <summary>
    /// 웨이브 시간 체크
    /// </summary>
    public void Check_WaveTime()
    {
        // Time.deltaTime을 이용해 매 프레임마다 시간을 누적하고 TimeSpan으로 변환
        _elapsedTime += TimeSpan.FromSeconds(Time.deltaTime);

        // 현재 웨이브 인덱스가 데이터 범위 내에 있는지 확인
        if (_curWaveIndex < _waveDatas.waveDataArr.Length)
        {
            // 현재 웨이브의 목표 시간을 가져옴
            TimeSpan waveTargetTime = TimeSpan.Parse(_waveDatas.waveDataArr[_curWaveIndex].Wave);

            // 경과 시간이 목표 웨이브 시간 이상이 되면 이벤트 발생
            if (_elapsedTime >= waveTargetTime)
            {
                // 웨이브 이벤트 실행
                WaveEvent(_waveDatas.waveDataArr[_curWaveIndex]);

                _elapsedTime = TimeSpan.Zero; // 경과 시간 초기화

                // 다음 웨이브 인덱스로 이동
                _curWaveIndex++;
            }
        }
    }

    /// <summary>
    /// 웨이브 이벤트
    /// </summary>
    private void WaveEvent(WaveData waveData)
    {
        Debug.Log($"{waveData.Wave} : 이벤트 발생!!!!!!!!!");

        //// 현재 웨이브의 몬스터 타입과 수량을 가져옴
        //string[] monsterTypes = waveData.MonsterType;
        //int[] quantities = waveData.Quantity;

        //// 몬스터 타입과 수량을 출력
        //for (int i = 0; i < monsterTypes.Length; i++)
        //{
        //    Debug.Log($"Wave {_curWaveIndex + 1} - MonsterType: {monsterTypes[i]}, Quantity: {quantities[i]}");
        //}
    }
}
