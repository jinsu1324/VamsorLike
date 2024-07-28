using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/NewMonster")]
public class MonsterData : ScriptableObject
{
    public string NAME;
    public int HP;
    public int ATK;
    public float SPEED;
}
