using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveManager : MonoBehaviour
{    
    [SerializeField]
    private WaveDatas _waveDatas;                               // 웨이브 데이터
    
    private int _curWaveIndex = 0;                              // 현재 웨이브 인덱스
    private float _playTime = 0;                                // 게임플레이시간

    private PlayTimeUI _playTimeUI;                             // 플레이타임 UI를 담을 변수

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        EmptyCheck_WaveDatas();

        _playTimeUI = PlaySceneManager.Instance.PlaySceneCanvas.PlayTimeUI;
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (PlaySceneManager.Instance.IsGameStart == false)
            return;

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
    /// 플레이타임 UI 갱신
    /// </summary>
    private void Refresh_PlayTimeUI(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        string playTime_Format =  string.Format("{0:00}:{1:00}", minutes, seconds);

        _playTimeUI.RefreshUIText(playTime_Format);
    }

    /// <summary>
    /// 웨이브 시간 체크
    /// </summary>
    public void Check_WaveTime()
    {
        // Time.deltaTime을 이용해 매 프레임마다 시간을 누적
        _playTime += Time.deltaTime;

        // 플레이타임 UI 갱신
        Refresh_PlayTimeUI(_playTime);

        // 현재 웨이브 인덱스가 데이터 범위 내에 있는지 확인
        if (_curWaveIndex < _waveDatas.waveDataArr.Length)
        {
            // 현재 웨이브의 목표 시간을 가져옴
            float waveTargetTime = _waveDatas.waveDataArr[_curWaveIndex].WaveTime;

            // 경과 시간이 목표 웨이브 시간 이상이 되면 이벤트 발생
            if (_playTime >= waveTargetTime)
            {
                // 웨이브 이벤트 실행
                WaveEvent(_waveDatas.waveDataArr[_curWaveIndex]);

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
        Debug.Log($"------{waveData.WaveTime} : 이벤트 발생!!!!!!!!!------");

        // 몬스터 스폰 (해당 웨이브의 몬스터 종류만큼 반복)
        for (int i = 0; i < waveData.MonsterType.Length; i++)
        {
            EnemySpawner.Instance.StartMonsterSpawn(waveData, i);
        }

        // 보스 있으면 보스 스폰
        string waveBoss = waveData.BossType;
        if (waveBoss != "None")
        {
            EnemySpawner.Instance.BossSpawn(waveData);
        }
    }
}
