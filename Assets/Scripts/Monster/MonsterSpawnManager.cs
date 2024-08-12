using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawnManager : SerializedMonoBehaviour
{
    // 필드에 스폰되어있는 몬스터들 리스트
    [SerializeField]
    private List<MonsterObject> _fieldSpwanMonsterList = new List<MonsterObject>();
    public List<MonsterObject> FieldSpawnMonsterList { get { return _fieldSpwanMonsterList; } set { _fieldSpwanMonsterList = value; } }

    // 스폰되고 있는지 확인할 bool
    private bool _isSpawned = false;
    public bool IsSpawned 
    { 
        get 
        { 
            return _isSpawned; 
        } 
        set 
        { 
            _isSpawned = value;
            StartCoroutine(MonsterRandomSpawn());
        } 
    }

    // 몬스터 스폰 딜레이
    [SerializeField]
    private float _spawnDelay;

    // 몬스터 스폰 거리
    [SerializeField]
    private float _spawnDistance;    

    // 몬스터 랜덤 스폰
    private IEnumerator MonsterRandomSpawn()
    {
        while (_isSpawned)
        {     
            // MonsterID Enum 값들을 다 가져와서 랜덤으로 하나만 뽑기
            MonsterID[] monsterIDValues = System.Enum.GetValues(typeof(MonsterID)) as MonsterID[];
            MonsterID randomMonsterID = (MonsterID)monsterIDValues.GetValue(Random.Range(0, monsterIDValues.Length));

            // 랜덤으로 뽑힌 몬스터와 현재 플레이중인 영웅
            MonsterObject pickedMonsterObject = PlaySceneManager.Instance.MonsterDataManager.MonsterObjectDict[randomMonsterID];
            HeroObject thisGameHeroObject = PlaySceneManager.Instance.ThisGameHeroObject;

            //영웅에서 스폰될 원형거리 구해서 저장
            Vector2 pickedRandomRadialPos = RandomCirclePos(thisGameHeroObject.transform.position, _spawnDistance);

            // 그 몬스터 ID에 맞는 MonsterObject를, 영웅위치에 생성하고 스탯도 넣어줌
            MonsterObject monsterObject = Instantiate(pickedMonsterObject, pickedRandomRadialPos, Quaternion.identity);
            monsterObject.Spawn();

            // 해당 필드에 소환되어있는 몬스터 리스트에도 넣어줌
            AddFieldMonsterList(monsterObject);

            // 스폰 딜레이만큼 대기
            yield return new WaitForSecondsRealtime(_spawnDelay);
        }
    }

    // 필드에 스폰되어있는 몬스터들 리스트에 추가해주는 함수
    private void AddFieldMonsterList(MonsterObject monsterObject)
    {
        _fieldSpwanMonsterList.Add(monsterObject);
    }

    // targetPos에서 distance 만큼 떨어진 원형 거리를 리턴해주는 함수
    private Vector3 RandomCirclePos(Vector2 targetPos, float distance)
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
