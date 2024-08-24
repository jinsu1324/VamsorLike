using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectPoolManager : MonoBehaviour
{
    [SerializeField]
    private ObjectPool _objectPool;

    [SerializeField]
    private float _spawnTime;

    private float _time;

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _spawnTime)
        {
            GameObject go = _objectPool.GetObj();
            _time = 0;
        }
    }

}
