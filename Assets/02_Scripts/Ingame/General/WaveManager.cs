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
    private float _elapsedTime = 0;                             // ����ð�

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
    /// ���̺� �ð� üũ
    /// </summary>
    public void Check_WaveTime()
    {
        // Time.deltaTime�� �̿��� �� �����Ӹ��� �ð��� ����
        _elapsedTime += Time.deltaTime;

        // ���� ���̺� �ε����� ������ ���� ���� �ִ��� Ȯ��
        if (_curWaveIndex < _waveDatas.waveDataArr.Length)
        {
            // ���� ���̺��� ��ǥ �ð��� ������
            float waveTargetTime = _waveDatas.waveDataArr[_curWaveIndex].WaveTime;

            // ��� �ð��� ��ǥ ���̺� �ð� �̻��� �Ǹ� �̺�Ʈ �߻�
            if (_elapsedTime >= waveTargetTime)
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
