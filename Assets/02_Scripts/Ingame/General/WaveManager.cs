using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{    
    [SerializeField]
    private WaveDatas _waveDatas;                               // ���̺� ������
    
    private int _curWaveIndex = 0;                              // ���� ���̺� �ε���
    private TimeSpan _elapsedTime = TimeSpan.Zero;              // ����ð�

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
        // Time.deltaTime�� �̿��� �� �����Ӹ��� �ð��� �����ϰ� TimeSpan���� ��ȯ
        _elapsedTime += TimeSpan.FromSeconds(Time.deltaTime);

        // ���� ���̺� �ε����� ������ ���� ���� �ִ��� Ȯ��
        if (_curWaveIndex < _waveDatas.waveDataArr.Length)
        {
            // ���� ���̺��� ��ǥ �ð��� ������
            TimeSpan waveTargetTime = TimeSpan.Parse(_waveDatas.waveDataArr[_curWaveIndex].Wave);

            // ��� �ð��� ��ǥ ���̺� �ð� �̻��� �Ǹ� �̺�Ʈ �߻�
            if (_elapsedTime >= waveTargetTime)
            {
                // ���̺� �̺�Ʈ ����
                WaveEvent(_waveDatas.waveDataArr[_curWaveIndex]);

                _elapsedTime = TimeSpan.Zero; // ��� �ð� �ʱ�ȭ

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
        Debug.Log($"{waveData.Wave} : �̺�Ʈ �߻�!!!!!!!!!");

        //// ���� ���̺��� ���� Ÿ�԰� ������ ������
        //string[] monsterTypes = waveData.MonsterType;
        //int[] quantities = waveData.Quantity;

        //// ���� Ÿ�԰� ������ ���
        //for (int i = 0; i < monsterTypes.Length; i++)
        //{
        //    Debug.Log($"Wave {_curWaveIndex + 1} - MonsterType: {monsterTypes[i]}, Quantity: {quantities[i]}");
        //}
    }
}
