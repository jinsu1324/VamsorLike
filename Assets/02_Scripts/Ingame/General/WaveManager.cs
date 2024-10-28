using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveManager : MonoBehaviour
{    
    [SerializeField]
    private WaveDatas _waveDatas;                               // ���̺� ������
    
    private int _curWaveIndex = 0;                              // ���� ���̺� �ε���
    private float _playTime = 0;                                // �����÷��̽ð�

    private PlayTimeUI _playTimeUI;                             // �÷���Ÿ�� UI�� ���� ����

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
    /// ���̺� ������ ���� üũ
    /// </summary>
    private void EmptyCheck_WaveDatas()
    {
        if (_waveDatas == null || _waveDatas.waveDataArr.Length == 0)
        {
            Debug.LogError("WaveDatas �� �������� �ʾҰų� �����Ͱ� ����ֽ��ϴ�!");
            enabled = false; // �����͸� �ҷ����� ���ϸ� ��ũ��Ʈ ��Ȱ��ȭ
        }
    }   

    /// <summary>
    /// �÷���Ÿ�� UI ����
    /// </summary>
    private void Refresh_PlayTimeUI(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        string playTime_Format =  string.Format("{0:00}:{1:00}", minutes, seconds);

        _playTimeUI.RefreshUIText(playTime_Format);
    }

    /// <summary>
    /// ���̺� �ð� üũ
    /// </summary>
    public void Check_WaveTime()
    {
        // Time.deltaTime�� �̿��� �� �����Ӹ��� �ð��� ����
        _playTime += Time.deltaTime;

        // �÷���Ÿ�� UI ����
        Refresh_PlayTimeUI(_playTime);

        // ���� ���̺� �ε����� ������ ���� ���� �ִ��� Ȯ��
        if (_curWaveIndex < _waveDatas.waveDataArr.Length)
        {
            // ���� ���̺��� ��ǥ �ð��� ������
            float waveTargetTime = _waveDatas.waveDataArr[_curWaveIndex].WaveTime;

            // ��� �ð��� ��ǥ ���̺� �ð� �̻��� �Ǹ� �̺�Ʈ �߻�
            if (_playTime >= waveTargetTime)
            {
                // ���̺� �̺�Ʈ ����
                WaveEvent(_waveDatas.waveDataArr[_curWaveIndex]);

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
            EnemySpawner.Instance.StartMonsterSpawn(waveData, i);
        }

        // ���� ������ ���� ����
        string waveBoss = waveData.BossType;
        if (waveBoss != "None")
        {
            EnemySpawner.Instance.BossSpawn(waveData);
        }
    }
}
