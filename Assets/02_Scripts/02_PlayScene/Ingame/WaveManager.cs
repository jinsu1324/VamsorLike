using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveManager : MonoBehaviour
{    
    private int _curWaveIndex = 0;                              // 현재 웨이브 인덱스
    private float _playTime = 0;                                // 게임플레이시간

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
    /// 플레이타임 UI 갱신
    /// </summary>
    private void Refresh_PlayTimeUI(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        string playTime_Format =  string.Format("{0:00}:{1:00}", minutes, seconds);

        PlaySceneCanvas.Instance.PlayTimeUI.RefreshUIText(playTime_Format);
    }

    /// <summary>
    /// 웨이브 시간 체크
    /// </summary>
    public void Check_WaveTime()
    {
        // 수정필요!
        // Time.deltaTime을 이용해 매 프레임마다 시간을 누적
        _playTime += Time.deltaTime;

        // 플레이타임 UI 갱신
        Refresh_PlayTimeUI(_playTime);

        // 현재 웨이브 인덱스가 데이터 범위 내에 있는지 확인
        if (_curWaveIndex < DataManager.Instance.WaveDatas.DataList.Count)
        {
            // 현재 웨이브의 목표 시간을 가져옴
            float waveTargetTime = ConvertTimeStringToFloat(DataManager.Instance.WaveDatas.DataList[_curWaveIndex].WaveTime);
            

            // 경과 시간이 목표 웨이브 시간 이상이 되면 이벤트 발생
            if (_playTime >= waveTargetTime)
            {
                // 웨이브 이벤트 실행
                WaveEvent(DataManager.Instance.WaveDatas.DataList[_curWaveIndex]);

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
            PlaySceneManager.Instance.EnemySpawner.StartMonsterSpawn(waveData, i);
        }

        // 보스 있으면 보스 스폰
        string waveBoss = waveData.BossType;
        if (waveBoss != "None")
        {
            PlaySceneManager.Instance.EnemySpawner.BossSpawn(waveData);
        }
    }

    /// <summary>
    /// 00:00:00 같은 string 시간 형식을 TimeSpan을 통해 float로 변환
    /// </summary>
    private float ConvertTimeStringToFloat(string timeString)
    {
        if (string.IsNullOrEmpty(timeString))
        {
            return -1.0f;
        }


        if (TimeSpan.TryParse(timeString, out TimeSpan timeSpan))
        {
            return (float)timeSpan.TotalSeconds;
        }
        else
        {
            Debug.LogError($"ConvertTimeStringToFloat 실패 : {timeString}");
            return -1.0f;
        }
    }
}
