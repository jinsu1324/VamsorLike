using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class MonsterObject : SerializedMonoBehaviour
{
    // 몬스터 데이터 스크립터블 오브젝트
    [SerializeField]
    private readonly MonsterData _monsterData;

    // 이름
    [SerializeField]
    private string _name;
    public string Name { get { return _name; } set { _name = value; } }

    // 체력
    [SerializeField]
    private int _hp;
    public int Hp { get { return _hp; } set { _hp = value; } }

    // 공격력
    [SerializeField]
    private int _atk;
    public int Atk { get { return _atk; } set { _atk = value; } }

    // 이동속도
    [SerializeField]
    private float _speed;
    public float Speed { get { return _speed; } set { _speed = value; } }


    private SpriteRenderer _spriteRenderer;

    // 스폰
    public void Spawn()
    {
        _name = _monsterData.Name;
        _hp = _monsterData.Hp;
        _atk = _monsterData.Atk;
        _speed = _monsterData.Speed;

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // HP 감소
    public void HPMinus(int atk)
    {
        _hp -= atk;
        StartCoroutine(Blink());

        if (_hp < 0)
            Death();
    }

    // 피격시 깜빡이기
    private IEnumerator Blink()
    {
        _spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _spriteRenderer.color = Color.white;
    }

    // 죽음
    private void Death()
    {
        Destroy(this.gameObject);
    }


    private void Update()
    {
        FollowHero();
    }


    // 영웅 따라다니도록
    private void FollowHero()
    {
        this.gameObject.transform.position = 
            Vector3.Lerp(this.gameObject.transform.position, 
            PlaySceneManager.ThisGameHeroObject.transform.position,
            _speed * Time.deltaTime);
    }

    // 영웅 충돌감지 및 공격
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            collision.gameObject.GetComponent<HeroObject>().HPMinus(_atk);
        }        
    }

}
