using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawnManager : SerializedMonoBehaviour
{
    // �ʵ忡 �����Ǿ��ִ� ���͵� ����Ʈ
    [SerializeField]
    private List<MonsterObject> _fieldSpwanMonsterList = new List<MonsterObject>();
    public List<MonsterObject> FieldSpawnMonsterList { get { return _fieldSpwanMonsterList; } set { _fieldSpwanMonsterList = value; } }

    // �����ǰ� �ִ��� Ȯ���� bool
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

    // ���� ���� ������
    [SerializeField]
    private float _spawnDelay;

    // ���� ���� �Ÿ�
    [SerializeField]
    private float _spawnDistance;    

    // ���� ���� ����
    private IEnumerator MonsterRandomSpawn()
    {
        while (_isSpawned)
        {     
            // MonsterID Enum ������ �� �����ͼ� �������� �ϳ��� �̱�
            MonsterID[] monsterIDValues = System.Enum.GetValues(typeof(MonsterID)) as MonsterID[];
            MonsterID randomMonsterID = (MonsterID)monsterIDValues.GetValue(Random.Range(0, monsterIDValues.Length));

            // �������� ���� ���Ϳ� ���� �÷������� ����
            MonsterObject pickedMonsterObject = PlaySceneManager.Instance.MonsterDataManager.MonsterObjectDict[randomMonsterID];
            HeroObject thisGameHeroObject = PlaySceneManager.Instance.ThisGameHeroObject;

            //�������� ������ �����Ÿ� ���ؼ� ����
            Vector2 pickedRandomRadialPos = RandomCirclePos(thisGameHeroObject.transform.position, _spawnDistance);

            // �� ���� ID�� �´� MonsterObject��, ������ġ�� �����ϰ� ���ȵ� �־���
            MonsterObject monsterObject = Instantiate(pickedMonsterObject, pickedRandomRadialPos, Quaternion.identity);
            monsterObject.Spawn();

            // �ش� �ʵ忡 ��ȯ�Ǿ��ִ� ���� ����Ʈ���� �־���
            AddFieldMonsterList(monsterObject);

            // ���� �����̸�ŭ ���
            yield return new WaitForSecondsRealtime(_spawnDelay);
        }
    }

    // �ʵ忡 �����Ǿ��ִ� ���͵� ����Ʈ�� �߰����ִ� �Լ�
    private void AddFieldMonsterList(MonsterObject monsterObject)
    {
        _fieldSpwanMonsterList.Add(monsterObject);
    }

    // targetPos���� distance ��ŭ ������ ���� �Ÿ��� �������ִ� �Լ�
    private Vector3 RandomCirclePos(Vector2 targetPos, float distance)
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
