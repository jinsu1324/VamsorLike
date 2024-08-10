using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPrefab : SerializedMonoBehaviour
{
    [SerializeField]
    private readonly MonsterData _monsterData;

    [SerializeField]
    private string _name;
    [SerializeField]
    private int _hp;
    [SerializeField]
    private int _atk;
    [SerializeField]
    private float _speed;

    public void Spawn()
    {
        _name = _monsterData.Name;
        _hp = _monsterData.Hp;
        _atk = _monsterData.Atk;
        _speed = _monsterData.Speed;
    }
}
