using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

// 몬스터 스폰해주는 기계 : 일정 시간마다 랜덤 스폰 / 스폰 딜레이 / 스폰 거리
public class EnemySpawner : SerializedMonoBehaviour
{
    [SerializeField]
    private float _spawnDistance;       // 몬스터 스폰 거리


    /// <summary>
    /// 몬스터 스폰
    /// </summary>
    public void StartMonsterSpawn(WaveData waveData, int index)
    {
        StartCoroutine(MonsterSpawn(waveData, index));
    }


    /// <summary>
    /// 몬스터 스폰 코루틴
    /// </summary>
    private IEnumerator MonsterSpawn(WaveData waveData, int index)
    {
        // 몬스터 토탈 스폰 카운트 만큼 반복
        for (int count = 0; count < waveData.TotalSpawnCount[index];)
        {
            // 팩토리에서 셋팅 + 풀에서 가져오기
            MonsterID monsterID = Enum.Parse<MonsterID>(waveData.MonsterType[index]);
            MonsterObj monsterObj = PlaySceneManager.Instance.EnemyFactory.SettingMonster(monsterID);

            // 스폰 위치 지정
            Vector2 randomCirclePos = RandomCircleSurfacePos(PlaySceneManager.Instance.MyHeroObj.transform.position, _spawnDistance);
            monsterObj.transform.position = randomCirclePos;
            //Debug.Log($"{monsterObj.name} : {count + 1} / {waveData.TotalSpawnCount[index]}");

            // 대기 후 카운트 올림
            yield return new WaitForSeconds(waveData.SpawnInterval[index]);
            count++;
        }
    }

    /// <summary>
    /// 보스 스폰
    /// </summary>
    public void BossSpawn(WaveData waveData)
    {
        // 팩토리에서 셋팅 + 풀에서 가져오기
        BossID bossID = Enum.Parse<BossID>(waveData.BossType);     
        BossObj boss = PlaySceneManager.Instance.EnemyFactory.SettingMonster(bossID);

        // HP Bar UI 초기화
        PlaySceneCanvas.Instance.BossHPBarUI.initialize(bossID);
        
        // 스폰 위치 지정
        Vector2 randomCirclePos = RandomCircleSurfacePos(PlaySceneManager.Instance.MyHeroObj.transform.position, _spawnDistance);
        boss.transform.position = randomCirclePos;
    }

    /// <summary>
    /// 적 다시 풀으로 돌려보내기
    /// </summary>
    public void EnemyBackTrans(Enemy enemy)
    {
        enemy.GetComponent<ObjectPoolObject>().BackTrans();
    }    

    /// <summary>
    /// targetPos에서 distance 만큼 떨어진 원의 표면중 랜덤한 포지션을 리턴해주는 함수
    /// </summary>
    public Vector3 RandomCircleSurfacePos(Vector2 targetPos, float distance)
    {
        // 원형 내부 임의의 지점 얻어옴
        Vector2 randomCirclePos = UnityEngine.Random.insideUnitCircle;

        //정규화해서 1로 만듦
        randomCirclePos.Normalize();

        // 거리를 곱해줘서 원하는 거리로 만듦
        randomCirclePos *= distance;

        // 그 값을 타겟위치에 더해줘서 최종 위치를 저장한 다음 리턴
        Vector2 targetRandomCirclePos = targetPos + randomCirclePos;
        return targetRandomCirclePos;
    }
}
