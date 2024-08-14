using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawner : SerializedMonoBehaviour
{
    // ���� ���� ������
    [SerializeField]
    private float _spawnDelay;

    // ���� ���� �Ÿ�
    [SerializeField]
    private float _spawnDistance;    


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
            // �������� ���� �Ѱ� �̱� (MonsterID �߿�)
            MonsterObject randomMonsterObject = RandomMonsterObject();

            // �������� �������� �����Ÿ� ������ ����������
            Vector2 randomCirclePos = RandomCircleSurfacePos(PlaySceneManager.ThisGameHeroObject.transform.position, _spawnDistance);

            // �� ���� ID�� �´� MonsterObject��, ������ġ�� �����ϰ� ���ȵ� �־���
            MonsterObject monsterObject = Instantiate(randomMonsterObject, randomCirclePos, Quaternion.identity);
            monsterObject.DataSetting();

            // ���� �����̸�ŭ ���
            yield return new WaitForSecondsRealtime(_spawnDelay);
        }
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
