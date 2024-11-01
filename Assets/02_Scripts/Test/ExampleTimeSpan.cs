using UnityEngine;
using System;

public class ExampleTimeSpan : MonoBehaviour
{
    private TimeSpan elapsedTime = TimeSpan.Zero; // ��� �ð��� 0�ʷ� �ʱ�ȭ
    private TimeSpan waveInterval = TimeSpan.FromSeconds(30); // 30�� �������� �̺�Ʈ �߻�

    void Update()
    {
        // Time.deltaTime�� �̿��� �� �����Ӹ��� �ð��� �����ϰ� TimeSpan���� ��ȯ
        elapsedTime += TimeSpan.FromSeconds(Time.deltaTime);

        // ��� �ð��� 30��(������ waveInterval) �̻��� �Ǿ����� Ȯ��
        if (elapsedTime >= waveInterval)
        {
            TriggerWaveEvent(); // Ư�� �̺�Ʈ ����
            elapsedTime = TimeSpan.Zero; // �̺�Ʈ �߻� �� ��� �ð��� �ʱ�ȭ
        }
    }

    // Ư�� �̺�Ʈ�� ��Ÿ���� �޼���
    private void TriggerWaveEvent()
    {
        Debug.Log("30�ʰ� ���� �̺�Ʈ�� �߻��߽��ϴ�!");
        // �̺�Ʈ ó�� ���� �ۼ�
    }
}

