using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class MonsterObject : SerializedMonoBehaviour
{
    [Title("데이터 본체", bold: false)]
    // 몬스터 오브젝트에 들어갈 데이터
    [SerializeField]
    private readonly MonsterData _monsterData;

    [Title("할당될 데이터 값들", bold: false)]
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

    // 스프라이트 렌더러
    private SpriteRenderer _spriteRenderer;

    private void FixedUpdate()
    {
        // 영웅 따라다니도록
        FollowHero();
    }

    // 데이터 셋팅
    public void DataSetting()
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

        // 스프라이트 깜빡이기
        BlinkSprite blinkSprite = new BlinkSprite();
        StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));

        if (_hp < 0)
            Death();
    }


    // 죽음
    private void Death()
    {
        Destroy(this.gameObject);
    }


    // 영웅 충돌감지 및 공격
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {
            collision.gameObject.GetComponent<HeroObject>().HPMinus(_atk);
        }
    }


    // 영웅 따라다니도록
    private void FollowHero()
    {
        this.gameObject.transform.position = 
            Vector3.Lerp(this.gameObject.transform.position, 
            PlaySceneManager.ThisGameHeroObject.transform.position,
            _speed * Time.deltaTime);
    }
}
