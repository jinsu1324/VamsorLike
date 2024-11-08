using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectName
{
    FX_Hit
}

public class EffectManager : MonoBehaviour
{
    // ����Ʈ ������ ����Ʈ
    [SerializeField]
    private List<GameObject> _effectPrefabList;

    // ����Ʈ ������ ����Ʈ -> ��ųʸ��� ��ȯ�� ����
    private Dictionary<string, GameObject> _effectPrefabDict = new Dictionary<string, GameObject>();           

    // Ǯ ��ųʸ�
    private Dictionary<string, Queue<GameObject>> _effectPoolDict = new Dictionary<string, Queue<GameObject>>();

    /// <summary>
    /// Awake
    /// </summary>
    private void Awake()
    {
        Initialize_EffectPrefabDict();
        Initialize_EffectPoolDict();
    }

    /// <summary>
    /// ����Ʈ ������ ��ųʸ� �ʱ�ȭ (����Ʈ ������ ����Ʈ�� ��ųʸ��� ����)
    /// </summary>
    private void Initialize_EffectPrefabDict()
    {
        foreach (var effectPrefab in _effectPrefabList)
        {
            if (_effectPrefabDict.ContainsKey(effectPrefab.name) == false)
            {
                _effectPrefabDict.Add(effectPrefab.name, effectPrefab);
            }
        }
    }

    /// <summary>
    /// ����Ʈ Ǯ ��ųʸ� �ʱ�ȭ (����Ʈ ������ ��ųʸ��� �����ؼ� Ǯ ��ųʸ��� ����)
    /// </summary>
    private void Initialize_EffectPoolDict()
    {
        foreach (var keyValuePair in _effectPrefabDict)
        {
            Create_EffectPoolDict(keyValuePair.Value, 20);
        }
    }

    /// <summary>
    /// ����Ʈ Ǯ ����
    /// </summary>
    private void Create_EffectPoolDict(GameObject effectPrefab, int count)
    {
        string effectName = effectPrefab.name;
        Queue<GameObject> pool = new Queue<GameObject>();

        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(effectPrefab, this.transform);
            obj.SetActive(false);
            obj.GetComponent<EffectPrefab>().Initialize(this, effectName);
            pool.Enqueue(obj);
        }

        _effectPoolDict.Add(effectName, pool);
    }

    /// <summary>
    /// ����Ʈ Ǯ���� ��������
    /// </summary>
    public GameObject GetEffect(string effectName, Vector3 position)
    {
        if (_effectPoolDict.TryGetValue(effectName, out Queue<GameObject> pool))
        {
            return GetEffectFromPool(effectName, pool, position);
        }
        else
        {
            Debug.LogWarning($"����Ʈ�� Ǯ�� �����ϴ�! '{effectName}' !");
            return null;
        }
    }

    /// <summary>
    /// Ǯ���� ����Ʈ �����ͼ� �����ǿ� ��ġ�ϰ� �ϴ� �Լ�
    /// </summary>
    private GameObject GetEffectFromPool(string effectName, Queue<GameObject> pool, Vector3 position)
    {
        // Ǯ�� ����Ʈ�� �ִٸ� �ٷ� ���� ���
        if (pool.Count > 0)
        {
            GameObject effectFromPool = pool.Dequeue();
            PositionEffect(effectFromPool, position);
            return effectFromPool;
        }

        // ��ųʸ����� �������� ã�� ����� bool ������ ����
        bool prefabExists = _effectPrefabDict.TryGetValue(effectName, out GameObject effectPrefab);

        // Ǯ�� ���µ�, ��ųʸ������� �������� ã�� ���� ���
        if (prefabExists == false)
        {
            Debug.LogWarning($"����Ʈ�� ��ųʸ��� �����ϴ� '{effectName}'");
            return null;
        }

        // ���� ������ ����Ʈ�� �ʱ�ȭ�ϰ� ��ȯ
        GameObject newEffect = Instantiate(effectPrefab);
        newEffect.GetComponent<EffectPrefab>().Initialize(this, effectName);
        PositionEffect(newEffect, position);
        return newEffect;
    }

    /// <summary>
    /// ����Ʈ�� ������ ��ġ�� �����ϰ� Ȱ��ȭ�ϴ� �Լ�
    /// </summary>
    private void PositionEffect(GameObject effect, Vector3 position)
    {
        effect.transform.position = position;
        effect.SetActive(true);
    }

    /// <summary>
    /// ����Ʈ �ٽ� Ǯ�� ����������
    /// </summary>
    public void BackPoolEffect(string effectName, GameObject effectPrefab)
    {
        effectPrefab.SetActive(false);

        if (_effectPoolDict.TryGetValue(effectName, out Queue<GameObject> pool))
        {
            pool.Enqueue(effectPrefab);
        }
        else
        {
            Debug.LogWarning($"����Ʈ Ǯ�� �������� �ʽ��ϴ�! '{effectName}'");
        }
    }
}
