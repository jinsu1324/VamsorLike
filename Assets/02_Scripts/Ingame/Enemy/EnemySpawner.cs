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
    #region 싱글톤_씬이동x
    private static EnemySpawner _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static EnemySpawner Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }

            return _instance;
        }
    }
    #endregion
        
    [SerializeField]
    private float _spawnDistance;       // 몬스터 스폰 거리


    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        // 몬스터 죽었을 때
        Enemy.OnEnemyDead += MonsterBackTrans;
    }

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
            MonsterID monsterID = Enum.Parse<MonsterID>(waveData.MonsterType[index]);
            MonsterObj monsterObj = EnemyFactory.Instance.SettingMonster(monsterID);

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
        BossID bossID = Enum.Parse<BossID>(waveData.BossType);     
        BossObj boss = EnemyFactory.Instance.SettingMonster(bossID);

        Vector2 randomCirclePos = RandomCircleSurfacePos(PlaySceneManager.Instance.MyHeroObj.transform.position, _spawnDistance);
        boss.transform.position = randomCirclePos;
    }

    /// <summary>
    /// 몬스터 다시 풀으로 돌려보내기
    /// </summary>
    public void MonsterBackTrans(Enemy monster)
    {
        monster.GetComponent<ObjectPoolObject>().BackTrans();
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

    /// <summary>
    /// 씬 전환되거나 오브젝트 파괴될 때 이벤트 제거
    /// </summary>
    public void OnDisable()
    {
        Enemy.OnEnemyDead -= MonsterBackTrans;
    }
}
