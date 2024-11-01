using UnityEngine;
using System;

public class ExampleTimeSpan : MonoBehaviour
{
    private TimeSpan elapsedTime = TimeSpan.Zero; // 경과 시간을 0초로 초기화
    private TimeSpan waveInterval = TimeSpan.FromSeconds(30); // 30초 간격으로 이벤트 발생

    void Update()
    {
        // Time.deltaTime을 이용해 매 프레임마다 시간을 누적하고 TimeSpan으로 변환
        elapsedTime += TimeSpan.FromSeconds(Time.deltaTime);

        // 경과 시간이 30초(설정한 waveInterval) 이상이 되었는지 확인
        if (elapsedTime >= waveInterval)
        {
            TriggerWaveEvent(); // 특정 이벤트 실행
            elapsedTime = TimeSpan.Zero; // 이벤트 발생 후 경과 시간을 초기화
        }
    }

    // 특정 이벤트를 나타내는 메서드
    private void TriggerWaveEvent()
    {
        Debug.Log("30초가 지나 이벤트가 발생했습니다!");
        // 이벤트 처리 로직 작성
    }
}

