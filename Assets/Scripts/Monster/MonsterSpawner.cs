using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ���� �������ִ� ��� : ���� �ð����� ���� ���� / ���� ������ / ���� �Ÿ�
public class MonsterSpawner : SerializedMonoBehaviour
{
    [SerializeField]
    private ObjectPool _objectPool;

    // ���� ���� ������
    [SerializeField]
    private float _spawnDelay;

    // ���� ���� �Ÿ�
    [SerializeField]
    private float _spawnDistance;


    private void Start()
    {
        MonsterObject.OnMonsterDeath += MonsterBackTrans;
    }

    // ���� ���� ����
    public void StartMonsterSpawn()
    {
        StartCoroutine(MonsterRandomSpawn());
    }

    // �����ð����� ���� ���� ���� �ڷ�ƾ
    private IEnumerator MonsterRandomSpawn()
    {
        while (PlaySceneManager.IsGameStart)
        {
            // �������� �������� �����Ÿ� ������ ����������
            Vector2 randomCirclePos = RandomCircleSurfacePos(PlaySceneManager.ThisGameHeroObject.transform.position, _spawnDistance);

            // ������Ʈ Ǯ���� ���� �����ͼ� ���� �����ǿ� �����ϰ� ������ �ʱ�ȭ����
            GameObject go = _objectPool.GetObj();
            go.transform.position = randomCirclePos;
            go.GetComponent<MonsterObject>().DataSetting();

            // ���� �����̸�ŭ ���
            yield return new WaitForSecondsRealtime(_spawnDelay);
        }
    }

    // ���� �ٽ� Ǯ���� ����������
    public void MonsterBackTrans(MonsterObject monsterObject)
    {
        monsterObject.GetComponent<ObjectPoolObject>().BackTrans();
    }

    // MonsterID Enum ������ �� �����ͼ� �������� �ϳ��� ���͸� �̱�
    public MonsterObject RandomMonsterObject()
    {
        MonsterID[] monsterIDValues = System.Enum.GetValues(typeof(MonsterID)) as MonsterID[];
        MonsterID randomMonsterID = (MonsterID)monsterIDValues.GetValue(Random.Range(0, monsterIDValues.Length));
        MonsterObject randomMonsterObject = Managers.Instance.ObjectManager.MonsterObjectDict[randomMonsterID];

        return randomMonsterObject;
    }

    // targetPos���� distance ��ŭ ������ ���� ǥ���� ������ �������� �������ִ� �Լ�
    private Vector3 RandomCircleSurfacePos(Vector2 targetPos, float distance)
    {
        // ���� ���� ������ ���� ����
        Vector2 randomCirclePos = Random.insideUnitCircle;

        //����ȭ�ؼ� 1�� ����
        randomCirclePos.Normalize();

        // �Ÿ��� �����༭ ���ϴ� �Ÿ��� ����
        randomCirclePos *= distance;

        // �� ���� Ÿ����ġ�� �����༭ ���� ��ġ�� ������ ���� ����
        Vector2 targetRandomCirclePos = targetPos + randomCirclePos;
        return targetRandomCirclePos;
    }

}
