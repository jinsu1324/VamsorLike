using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ���� �������ִ� ��� : ���� �ð����� ���� ���� / ���� ������ / ���� �Ÿ�
public class MonsterSpawner : SerializedMonoBehaviour
{
    #region �̱���_���̵�x
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
    private float _spawnDistance;       // ���� ���� �Ÿ�


    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        // ���� �׾��� ��
        MonsterObject.OnMonsterDeath += MonsterBackTrans;
    }

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    public void StartMonsterSpawn()
    {
        StartCoroutine(MonsterRandomSpawn());
    }

    /// <summary>
    /// �����ð����� ���� ���� ���� �ڷ�ƾ
    /// </summary>
    private IEnumerator MonsterRandomSpawn()
    {
        while (PlaySceneManager.Instance.IsGameStart)
        {
            // �������� �������� �����Ÿ� ������ ����������
            Vector2 randomCirclePos = RandomCircleSurfacePos(PlaySceneManager.Instance.MyHeroObj.transform.position, _spawnDistance);

            // �������� ������ �°� ���� ID ��������
            MonsterID monsterID_by_StageLevel = GetMonsterID_by_StageLevel(PlaySceneManager.Instance.StageLevel);

            // �������� ������ �°� ���������� ��������
            float spawnDelay_by_StageLevel = GetSpawnDelay_by_StageLevel(PlaySceneManager.Instance.StageLevel);

            // �ѵ������� ���� + Ǯ���� ��������
            MonsterObject settingCompleteRandomMonster = MonsterFactory.Instance.SettingMonster(monsterID_by_StageLevel);

            // ���� ���� ���������� ��ġ ����
            settingCompleteRandomMonster.transform.position = randomCirclePos;

            // ���� �����̸�ŭ ���
            yield return new WaitForSecondsRealtime(spawnDelay_by_StageLevel);
        }
    }

    /// <summary>
    /// ���� �ٽ� Ǯ���� ����������
    /// </summary>
    public void MonsterBackTrans(MonsterObject monsterObject)
    {
        monsterObject.GetComponent<ObjectPoolObject>().BackTrans();
    }    

    /// <summary>
    /// �������� ������ ���� ���� ���̵� ��ȯ
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
    /// ���� ���� ������ ����
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
    /// targetPos���� distance ��ŭ ������ ���� ǥ���� ������ �������� �������ִ� �Լ�
    /// </summary>
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

    /// <summary>
    /// �� ��ȯ�ǰų� ������Ʈ �ı��� �� �̺�Ʈ ����
    /// </summary>
    public void OnDisable()
    {
        MonsterObject.OnMonsterDeath -= MonsterBackTrans;
    }
}
