using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveManager : MonoBehaviour
{    
    private int _curWaveIndex = 0;                              // ���� ���̺� �ε���
    private float _playTime = 0;                                // �����÷��̽ð�

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
    /// �÷���Ÿ�� UI ����
    /// </summary>
    private void Refresh_PlayTimeUI(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        string playTime_Format =  string.Format("{0:00}:{1:00}", minutes, seconds);

        PlaySceneCanvas.Instance.PlayTimeUI.RefreshUIText(playTime_Format);
    }

    /// <summary>
    /// ���̺� �ð� üũ
    /// </summary>
    public void Check_WaveTime()
    {
        // �����ʿ�!
        // Time.deltaTime�� �̿��� �� �����Ӹ��� �ð��� ����
        _playTime += Time.deltaTime;

        // �÷���Ÿ�� UI ����
        Refresh_PlayTimeUI(_playTime);

        // ���� ���̺� �ε����� ������ ���� ���� �ִ��� Ȯ��
        if (_curWaveIndex < DataManager.Instance.WaveDatas.DataList.Count)
        {
            // ���� ���̺��� ��ǥ �ð��� ������
            float waveTargetTime = ConvertTimeStringToFloat(DataManager.Instance.WaveDatas.DataList[_curWaveIndex].WaveTime);
            

            // ��� �ð��� ��ǥ ���̺� �ð� �̻��� �Ǹ� �̺�Ʈ �߻�
            if (_playTime >= waveTargetTime)
            {
                // ���̺� �̺�Ʈ ����
                WaveEvent(DataManager.Instance.WaveDatas.DataList[_curWaveIndex]);

                // ���� ���̺� �ε����� �̵�
                _curWaveIndex++;
            }
        }
    }

    /// <summary>
    /// ���̺� �̺�Ʈ
    /// </summary>
    private void WaveEvent(WaveData waveData)
    {
        Debug.Log($"------{waveData.WaveTime} : �̺�Ʈ �߻�!!!!!!!!!------");

        // ���� ���� (�ش� ���̺��� ���� ������ŭ �ݺ�)
        for (int i = 0; i < waveData.MonsterType.Length; i++)
        {
            PlaySceneManager.Instance.EnemySpawner.StartMonsterSpawn(waveData, i);
        }

        // ���� ������ ���� ����
        string waveBoss = waveData.BossType;
        if (waveBoss != "None")
        {
            PlaySceneManager.Instance.EnemySpawner.BossSpawn(waveData);
        }
    }

    /// <summary>
    /// 00:00:00 ���� string �ð� ������ TimeSpan�� ���� float�� ��ȯ
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
            Debug.LogError($"ConvertTimeStringToFloat ���� : {timeString}");
            return -1.0f;
        }
    }
}
