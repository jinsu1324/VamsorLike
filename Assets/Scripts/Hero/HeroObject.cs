using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroObject : SerializedMonoBehaviour
{
    [SerializeField]
    private readonly HeroData _heroData;

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

    // 사거리
    [SerializeField]
    private float _range;
    public float Range { get { return _range; } set { _range = value; } }

    // 사거리
    [SerializeField]
    private float _delay;
    public float Delay { get { return _delay; } set { _delay = value; } }


    // 공격범위 콜라이더
    [SerializeField]
    private BoxCollider2D _attackRangeCollider;

    // 리지드바디와 움직임 방향
    private Rigidbody2D _rigid;
    private Vector2 _moveDir;

    private SpriteRenderer _spriteRenderer;

    private void FixedUpdate()
    {
        Move();
    }

    // 스폰
    public void Spawn()
    {
        // 데이터 넣어주기
        _name = _heroData.Name;
        _hp = _heroData.Hp;
        _atk = _heroData.Atk;
        _speed = _heroData.Speed;
        _range = _heroData.Range;
        _delay = _heroData.Delay;
        _attackRangeCollider.size = _attackRangeCollider.size * _range;


        // 필요 컴포넌트
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _moveDir = Vector2.zero;


        // 공격시작
        StartCoroutine(Attack());
    }

    // 이동
    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _moveDir.x = _rigid.position.x + (horizontal * _speed * Time.deltaTime);
        _moveDir.y = _rigid.position.y + (vertical * _speed * Time.deltaTime);

        _rigid.MovePosition(_moveDir);
    }    

    // 공격 코루틴
    private IEnumerator Attack()
    {
        while (true)
        {         
            Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _attackRangeCollider.size, 0.0f);

            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.tag == "Monster")
                {
                    hit.GetComponent<MonsterObject>().HPMinus(_atk);
                }
                else
                {
                    continue;
                }
            }

            yield return new WaitForSeconds(_delay);
        }
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
        Debug.Log("게임오버!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }


    // 사거리 기즈모로 표시
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _attackRangeCollider.size);
    }  
}
