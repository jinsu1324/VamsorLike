using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObject : SerializedMonoBehaviour
{
    [SerializeField]
    private readonly MonsterData _monsterData;

    [SerializeField]
    private string _name;
    public string Name { get { return _name; } set { _name = value; } }

    [SerializeField]
    private int _hp;
    public int Hp { get { return _hp; } set { _hp = value; } }

    [SerializeField]
    private int _atk;
    public int Atk { get { return _atk; } set { _atk = value; } }

    [SerializeField]
    private float _speed;
    public float Speed { get { return _speed; } set { _speed = value; } }

    // ½ºÆù
    public void Spawn()
    {
        _name = _monsterData.Name;
        _hp = _monsterData.Hp;
        _atk = _monsterData.Atk;
        _speed = _monsterData.Speed;
    }
    
}
