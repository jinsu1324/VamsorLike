using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroObject : SerializedMonoBehaviour
{
    [SerializeField]
    private readonly HeroData _heroData;

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


    // 스폰
    public void Spawn()
    {
        _name = _heroData.Name;
        _hp = _heroData.Hp;
        _atk = _heroData.Atk;
        _speed = _heroData.Speed;
    }


    // 공격
    public void Attack(int monsterHp)
    {
        monsterHp -= _atk;
    }
}
