using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

// ���� �������ִ� ��� : ���� �ð����� ���� ���� / ���� ������ / ���� �Ÿ�
public class EnemySpawner : SerializedMonoBehaviour
{
    #region �̱���_���̵�x
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
    private float _spawnDistance;       // ���� ���� �Ÿ�


    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        // ���� �׾��� ��
        Enemy.OnEnemyDead += MonsterBackTrans;
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void StartMonsterSpawn(WaveData waveData, int index)
    {
        StartCoroutine(MonsterSpawn(waveData, index));
    }


    /// <summary>
    /// ���� ���� �ڷ�ƾ
    /// </summary>
    private IEnumerator MonsterSpawn(WaveData waveData, int index)
    {
        // ���� ��Ż ���� ī��Ʈ ��ŭ �ݺ�
        for (int count = 0; count < waveData.TotalSpawnCount[index];)
        {
            MonsterID monsterID = Enum.Parse<MonsterID>(waveData.MonsterType[index]);
            MonsterObj monsterObj = EnemyFactory.Instance.SettingMonster(monsterID);

            Vector2 randomCirclePos = RandomCircleSurfacePos(PlaySceneManager.Instance.MyHeroObj.transform.position, _spawnDistance);
            monsterObj.transform.position = randomCirclePos;

            //Debug.Log($"{monsterObj.name} : {count + 1} / {waveData.TotalSpawnCount[index]}");

            // ��� �� ī��Ʈ �ø�
            yield return new WaitForSeconds(waveData.SpawnInterval[index]);
            count++;
        }
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void BossSpawn(WaveData waveData)
    {
        BossID bossID = Enum.Parse<BossID>(waveData.BossType);     
        BossObj boss = EnemyFactory.Instance.SettingMonster(bossID);

        Vector2 randomCirclePos = RandomCircleSurfacePos(PlaySceneManager.Instance.MyHeroObj.transform.position, _spawnDistance);
        boss.transform.position = randomCirclePos;
    }

    /// <summary>
    /// ���� �ٽ� Ǯ���� ����������
    /// </summary>
    public void MonsterBackTrans(Enemy monster)
    {
        monster.GetComponent<ObjectPoolObject>().BackTrans();
    }    

    /// <summary>
    /// targetPos���� distance ��ŭ ������ ���� ǥ���� ������ �������� �������ִ� �Լ�
    /// </summary>
    public Vector3 RandomCircleSurfacePos(Vector2 targetPos, float distance)
    {
        // ���� ���� ������ ���� ����
        Vector2 randomCirclePos = UnityEngine.Random.insideUnitCircle;

        //����ȭ�ؼ� 1�� ����
        randomCirclePos.Normalize();

        // �Ÿ��� �����༭ ���ϴ� �Ÿ��� ����
        randomCirclePos *= distance;

        // �� ���� Ÿ����ġ�� �����༭ ���� ��ġ�� ������ ���� ����
        Vector2 targetRandomCirclePos = targetPos + randomCirclePos;
        return targetRandomCirclePos;
    }

    /// <summary>
    /// �� ��ȯ�ǰų� ������Ʈ �ı��� �� �̺�Ʈ ����
    /// </summary>
    public void OnDisable()
    {
        Enemy.OnEnemyDead -= MonsterBackTrans;
    }
}
