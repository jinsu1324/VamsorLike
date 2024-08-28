using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 몬스터 스폰해주는 기계 : 일정 시간마다 랜덤 스폰 / 스폰 딜레이 / 스폰 거리
public class MonsterSpawner : SerializedMonoBehaviour
{
    [SerializeField]
    private ObjectPool _objectPool;

    // 몬스터 스폰 딜레이
    [SerializeField]
    private float _spawnDelay;

    // 몬스터 스폰 거리
    [SerializeField]
    private float _spawnDistance;


    private void Start()
    {
        MonsterObject.OnMonsterDeath += MonsterBackTrans;
    }

    // 몬스터 스폰 시작
    public void StartMonsterSpawn()
    {
        StartCoroutine(MonsterRandomSpawn());
    }

    // 일정시간마다 몬스터 랜덤 스폰 코루틴
    private IEnumerator MonsterRandomSpawn()
    {
        while (PlaySceneManager.IsGameStart)
        {
            // 영웅에서 원형으로 일정거리 떨어진 랜덤포지션
            Vector2 randomCirclePos = RandomCircleSurfacePos(PlaySceneManager.ThisGameHeroObject.transform.position, _spawnDistance);

            // 오브젝트 풀에서 몬스터 가져와서 원형 포지션에 스폰하고 데이터 초기화까지
            GameObject go = _objectPool.GetObj();
            go.transform.position = randomCirclePos;
            go.GetComponent<MonsterObject>().DataSetting();

            // 스폰 딜레이만큼 대기
            yield return new WaitForSecondsRealtime(_spawnDelay);
        }
    }

    // 몬스터 다시 풀으로 돌려보내기
    public void MonsterBackTrans(MonsterObject monsterObject)
    {
        monsterObject.GetComponent<ObjectPoolObject>().BackTrans();
    }

    // MonsterID Enum 값들을 다 가져와서 랜덤으로 하나의 몬스터만 뽑기
    public MonsterObject RandomMonsterObject()
    {
        MonsterID[] monsterIDValues = System.Enum.GetValues(typeof(MonsterID)) as MonsterID[];
        MonsterID randomMonsterID = (MonsterID)monsterIDValues.GetValue(Random.Range(0, monsterIDValues.Length));
        MonsterObject randomMonsterObject = Managers.Instance.ObjectManager.MonsterObjectDict[randomMonsterID];

        return randomMonsterObject;
    }

    // targetPos에서 distance 만큼 떨어진 원의 표면중 랜덤한 포지션을 리턴해주는 함수
    private Vector3 RandomCircleSurfacePos(Vector2 targetPos, float distance)
    {
        // 원형 내부 임의의 지점 얻어옴
        Vector2 randomCirclePos = Random.insideUnitCircle;

        //정규화해서 1로 만듦
        randomCirclePos.Normalize();

        // 거리를 곱해줘서 원하는 거리로 만듦
        randomCirclePos *= distance;

        // 그 값을 타겟위치에 더해줘서 최종 위치를 저장한 다음 리턴
        Vector2 targetRandomCirclePos = targetPos + randomCirclePos;
        return targetRandomCirclePos;
    }

}
