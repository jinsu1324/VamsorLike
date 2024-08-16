using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 영웅 게임오브젝트 : 영웅관련 데이터 / 본인 데이터 이닛 / 공격 / 이동 / HP감소 / 죽음
public class HeroObject : SerializedMonoBehaviour
{
    [Title("데이터 본체", bold: false)]
    // 영웅 오브젝트에 들어갈 데이터
    [SerializeField]
    private readonly HeroData _heroData;

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

    // 사거리
    [SerializeField]
    private float _range;
    public float Range { get { return _range; } set { _range = value; } }

    // 공격 딜레이
    [SerializeField]
    private float _delay;
    public float Delay { get { return _delay; } set { _delay = value; } }

    [Title("필요한 컴포넌트들", bold: false)]
    // 공격범위 콜라이더
    [SerializeField]
    private BoxCollider2D _attackRangeCollider;

    // 리지드바디
    private Rigidbody2D _rigid;

    // 스프라이트 렌더러
    private SpriteRenderer _spriteRenderer;

    // 이동에 사용할 vector2 dir
    private Vector2 _moveDir;

    [SerializeField]
    private float _attackRange;

    float time = 0;

    private List<HeroSkillBase> _skillList = new List<HeroSkillBase>();

    private void FixedUpdate()
    {
        // 이동
        Move();

        Attack();

    }

    // 데이터 셋팅
    public void DataSetting()
    {
        // 데이터 넣어주기
        _name = _heroData.Name;
        _hp = _heroData.Hp;
        _atk = _heroData.Atk;
        _speed = _heroData.Speed;
        _range = _heroData.Range;
        _delay = _heroData.Delay;
        _attackRangeCollider.size = _attackRangeCollider.size * _range;

        // 필요 컴포넌트들 가져와서 할당
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _moveDir = Vector2.zero;


        HeroSKillSword heroSkillSword = new HeroSKillSword(_atk, _range, _delay);
        _skillList.Add(heroSkillSword);

        HeroSKillBow heroSKillBow = new HeroSKillBow(_atk, _range, _delay);
        _skillList.Add(heroSKillBow);
    }

    // 공격 시작
    public void AttackStart()
    {
        //StartCoroutine(Attack());
    }


    // 이동
    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _moveDir.x = _rigid.position.x + (horizontal * _speed * Time.deltaTime);
        _moveDir.y = _rigid.position.y + (vertical * _speed * Time.deltaTime);

        _rigid.MovePosition(_moveDir);        
    }


    // 공격 코루틴
    private void Attack()
    {        
        // 게임 시작되면 반복
        if (PlaySceneManager.IsGameStart)
        {

            for (int i = 0; i < _skillList.Count; i++)
            {
                HeroSkillBase skill = _skillList[i];

                if (skill.SkillUpdate())
                {
                    skill.AttackFunc(transform.position);
                }


            }
        }
    }

    IEnumerator coroutine;
    // HP 감소
    public void HPMinus(int atk)
    {
        _hp -= atk;

        // 스프라이트 깜빡이기

        if(coroutine != null)
            StopCoroutine(coroutine);

        coroutine = BlinkSprite.Blink(_spriteRenderer, 0.1f);
        StartCoroutine(coroutine);

        if (_hp < 0)
            Death();
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
        Gizmos.DrawSphere(transform.position, _attackRange);
    }  
}
