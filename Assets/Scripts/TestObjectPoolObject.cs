using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectPoolObject : ObjectPoolObject
{
    private float _lifeTime;
    private float _time;

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _lifeTime)
        {
            BackTrans();
        }
    }

    public override void Spawn()
    {
        _lifeTime = Random.Range(1.0f, 10.0f);
        _time = 0;

        base.Spawn();
    }
}
