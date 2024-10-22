using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 몬스터 스폰해주는 기계 : 일정 시간마다 랜덤 스폰 / 스폰 딜레이 / 스폰 거리
public class MonsterSpawner : SerializedMonoBehaviour
{
    #region 싱글톤_씬이동x
    private static MonsterSpawner _instance;

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

    public static MonsterSpawner Instance
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
        MonsterObject.OnMonsterDeath += MonsterBackTrans;
    }

    /// <summary>
    /// 몬스터 스폰 시작
    /// </summary>
    public void StartMonsterSpawn()
    {
        StartCoroutine(MonsterRandomSpawn());
    }

    /// <summary>
    /// 일정시간마다 몬스터 랜덤 스폰 코루틴
    /// </summary>
    private IEnumerator MonsterRandomSpawn()
    {
        while (PlaySceneManager.Instance.IsGameStart)
        {
            // 영웅에서 원형으로 일정거리 떨어진 랜덤포지션
            Vector2 randomCirclePos = RandomCircleSurfacePos(PlaySceneManager.Instance.MyHeroObj.transform.position, _spawnDistance);

            // 스테이지 레벨에 맞게 몬스터 ID 가져오기
            MonsterID monsterID_by_StageLevel = GetMonsterID_by_StageLevel(PlaySceneManager.Instance.StageLevel);

            // 스테이지 레벨에 맞게 스폰딜레이 가져오기
            float spawnDelay_by_StageLevel = GetSpawnDelay_by_StageLevel(PlaySceneManager.Instance.StageLevel);

            // 팩도리에서 셋팅 + 풀에서 꺼내오기
            MonsterObject settingCompleteRandomMonster = MonsterFactory.Instance.SettingMonster(monsterID_by_StageLevel);

            // 랜덤 원형 포지션으로 위치 설정
            settingCompleteRandomMonster.transform.position = randomCirclePos;

            // 스폰 딜레이만큼 대기
            yield return new WaitForSecondsRealtime(spawnDelay_by_StageLevel);
        }
    }

    /// <summary>
    /// 몬스터 다시 풀으로 돌려보내기
    /// </summary>
    public void MonsterBackTrans(MonsterObject monsterObject)
    {
        monsterObject.GetComponent<ObjectPoolObject>().BackTrans();
    }    

    /// <summary>
    /// 스테이지 레벨에 따른 몬스터 아이디 반환
    /// </summary>
    private MonsterID GetMonsterID_by_StageLevel(int stageLevel)
    {
        switch (stageLevel)
        {
            case 1:
                return MonsterID.Golem;
            case 2:
                return MonsterID.Skeleton;
            case 3:
                return MonsterID.Witch;
            case 4:
                return MonsterID.Dragon;
            default:
                return MonsterID.Golem;
        }
    }

    /// <summary>
    /// 몬스터 스폰 딜레이 줄임
    /// </summary>
    public float GetSpawnDelay_by_StageLevel(int stageLevel)
    {
        switch (stageLevel)
        {
            case 1:
                return 1.0f;
            case 2:
                return 0.95f;
            case 3:
                return 0.9f;
            case 4:
                return 0.85f;
            default:
                return 1.0f;
        }
    }

    /// <summary>
    /// targetPos에서 distance 만큼 떨어진 원의 표면중 랜덤한 포지션을 리턴해주는 함수
    /// </summary>
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

    /// <summary>
    /// 씬 전환되거나 오브젝트 파괴될 때 이벤트 제거
    /// </summary>
    public void OnDisable()
    {
        MonsterObject.OnMonsterDeath -= MonsterBackTrans;
    }
}
