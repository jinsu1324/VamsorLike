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
    // 이펙트 프리팹 리스트
    [SerializeField]
    private List<GameObject> _effectPrefabList;

    // 이펙트 프리팹 리스트 -> 딕셔너리로 변환할 변수
    private Dictionary<string, GameObject> _effectPrefabDict = new Dictionary<string, GameObject>();           

    // 풀 딕셔너리
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
    /// 이펙트 프리팹 딕셔너리 초기화 (이펙트 프리팹 리스트를 딕셔너리로 저장)
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
    /// 이펙트 풀 딕셔너리 초기화 (이펙트 프리팹 딕셔너리를 참조해서 풀 딕셔너리로 저장)
    /// </summary>
    private void Initialize_EffectPoolDict()
    {
        foreach (var keyValuePair in _effectPrefabDict)
        {
            Create_EffectPoolDict(keyValuePair.Value, 20);
        }
    }

    /// <summary>
    /// 이펙트 풀 생성
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
    /// 이펙트 풀에서 가져오기
    /// </summary>
    public GameObject GetEffect(string effectName, Vector3 position)
    {
        if (_effectPoolDict.TryGetValue(effectName, out Queue<GameObject> pool))
        {
            return GetEffectFromPool(effectName, pool, position);
        }
        else
        {
            Debug.LogWarning($"이펙트가 풀에 없습니다! '{effectName}' !");
            return null;
        }
    }

    /// <summary>
    /// 풀에서 이펙트 가져와서 포지션에 위치하게 하는 함수
    /// </summary>
    private GameObject GetEffectFromPool(string effectName, Queue<GameObject> pool, Vector3 position)
    {
        // 풀에 이펙트가 있다면 바로 꺼내 사용
        if (pool.Count > 0)
        {
            GameObject effectFromPool = pool.Dequeue();
            PositionEffect(effectFromPool, position);
            return effectFromPool;
        }

        // 딕셔너리에서 프리팹을 찾는 결과를 bool 변수에 저장
        bool prefabExists = _effectPrefabDict.TryGetValue(effectName, out GameObject effectPrefab);

        // 풀에 없는데, 딕셔너리에서도 프리팹을 찾지 못한 경우
        if (prefabExists == false)
        {
            Debug.LogWarning($"이펙트가 딕셔너리에 없습니다 '{effectName}'");
            return null;
        }

        // 새로 생성한 이펙트를 초기화하고 반환
        GameObject newEffect = Instantiate(effectPrefab);
        newEffect.GetComponent<EffectPrefab>().Initialize(this, effectName);
        PositionEffect(newEffect, position);
        return newEffect;
    }

    /// <summary>
    /// 이펙트를 지정된 위치에 설정하고 활성화하는 함수
    /// </summary>
    private void PositionEffect(GameObject effect, Vector3 position)
    {
        effect.transform.position = position;
        effect.SetActive(true);
    }

    /// <summary>
    /// 이펙트 다시 풀로 돌려보내기
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
            Debug.LogWarning($"이펙트 풀이 존재하지 않습니다! '{effectName}'");
        }
    }
}
