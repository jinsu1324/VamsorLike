using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPrefab : SerializedMonoBehaviour
{
    [SerializeField]
    private readonly HeroData _heroData;

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
        _name = _heroData.Name;
        _hp = _heroData.Hp;
        _atk = _heroData.Atk;
        _speed = _heroData.Speed;
    }
}
