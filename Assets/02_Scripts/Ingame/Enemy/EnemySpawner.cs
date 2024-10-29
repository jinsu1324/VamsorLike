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
    // ���� ���� �Ÿ�
    [SerializeField]
    private float _spawnDistance;       

    // ���� ������Ʈ Ǯ ��ųʸ�
    [SerializeField]
    private Dictionary<MonsterID, ObjectPool> _monsterObjectPoolDict = new Dictionary<MonsterID, ObjectPool>();

    // ���� ������Ʈ Ǯ ��ųʸ�
    [SerializeField]
    private Dictionary<BossID, ObjectPool> _bossObjectPoolDict = new Dictionary<BossID, ObjectPool>();


    /// <summary>
    /// ���� ������ �����ؼ� ����
    /// </summary>
    public MonsterObj SettingMonster(MonsterID monsterID)
    {
        // ������Ʈ Ǯ���� ���� ������
        GameObject go = _monsterObjectPoolDict[monsterID].GetObj();

        // ���� ������ �ʱ�ȭ (����)
        MonsterObj monster = go.GetComponent<MonsterObj>();
        monster.DataSetting();

        // �� ���͸� ��ȯ
        return monster;
    }

    /// <summary>
    /// ���� ������ �����ؼ� ����
    /// </summary>
    public BossObj SettingMonster(BossID bossID)
    {
        // ������Ʈ Ǯ���� ���� ������
        GameObject go = _bossObjectPoolDict[bossID].GetObj();

        // ���� ������ �ʱ�ȭ (����)
        BossObj boss = go.GetComponent<BossObj>();
        boss.DataSetting();

        // �� ������ ��ȯ
        return boss;
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
            // ���丮���� ���� + Ǯ���� ��������
            MonsterID monsterID = Enum.Parse<MonsterID>(waveData.MonsterType[index]);
            MonsterObj monsterObj = SettingMonster(monsterID);

            // ���� ��ġ ����
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
        // ���丮���� ���� + Ǯ���� ��������
        BossID bossID = Enum.Parse<BossID>(waveData.BossType);     
        BossObj boss = SettingMonster(bossID);

        // HP Bar UI �ʱ�ȭ
        PlaySceneCanvas.Instance.BossHPBarUI.initialize(bossID);
        
        // ���� ��ġ ����
        Vector2 randomCirclePos = RandomCircleSurfacePos(PlaySceneManager.Instance.MyHeroObj.transform.position, _spawnDistance);
        boss.transform.position = randomCirclePos;
    }

    /// <summary>
    /// �� �ٽ� Ǯ���� ����������
    /// </summary>
    public void EnemyBackTrans(Enemy enemy)
    {
        enemy.GetComponent<ObjectPoolObject>().BackTrans();
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
}
