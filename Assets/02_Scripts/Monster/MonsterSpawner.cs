using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ���� �������ִ� ��� : ���� �ð����� ���� ���� / ���� ������ / ���� �Ÿ�
public class MonsterSpawner : SerializedMonoBehaviour
{
    #region �̱���
    private static MonsterSpawner _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
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
        while (PlaySceneManager.Instance.IsGameStart)
        {
            // �������� �������� �����Ÿ� ������ ����������
            Vector2 randomCirclePos = RandomCircleSurfacePos(PlaySceneManager.ThisGameHeroObject.transform.position, _spawnDistance);

            // �ѵ������� ���ÿϷ�� ��������
            MonsterObject settingCompleteRandomMonster = MonsterFactory.Instance.SettingMonster(RandomMonsterID());

            // ���� ���� ���������� ��ġ ����
            settingCompleteRandomMonster.transform.position = randomCirclePos;

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
    public MonsterID RandomMonsterID()
    {
        MonsterID[] monsterIDValues = System.Enum.GetValues(typeof(MonsterID)) as MonsterID[];
        MonsterID randomMonsterID = (MonsterID)monsterIDValues.GetValue(Random.Range(0, monsterIDValues.Length));

        return randomMonsterID;
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
