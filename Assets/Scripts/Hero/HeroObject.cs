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
    public string Name { get; set; }

    // 체력
    [SerializeField]
    public int Hp { get; set; }

    // 공격력
    [SerializeField]
    public int Atk { get; set; }

    // 이동속도
    [SerializeField]
    public float Speed { get; set; }

    // 사거리
    [SerializeField]
    public float Range { get; set; }

    // 공격 딜레이
    [SerializeField]
    public float Delay { get; set; }


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


    private void FixedUpdate()
    {
        if (PlaySceneManager.IsGameStart)
        {
            Move();
            Attack();
        }            
    }

    // 데이터 셋팅
    public void DataSetting()
    {
        // 데이터 넣어주기
        Name = _heroData.Name;
        Hp = _heroData.Hp;
        Atk = _heroData.Atk;
        Speed = _heroData.Speed;
        Range = _heroData.Range;
        Delay = _heroData.Delay;
        _attackRangeCollider.size = _attackRangeCollider.size * Range;

        // 필요 컴포넌트들 가져와서 할당
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _moveDir = Vector2.zero;
    }


    // 이동
    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _moveDir.x = _rigid.position.x + (horizontal * Speed * Time.deltaTime);
        _moveDir.y = _rigid.position.y + (vertical * Speed * Time.deltaTime);

        _rigid.MovePosition(_moveDir);        
    }


    // 공격
    private void Attack()
    {
        // 스킬리스트에 아무것도 없으면 그냥 리턴
        if (HeroEquipedSkill.Instance.EquipedSkillList.Count == 0)
        {
            Debug.Log("EquipedSkillList Count 0");
            return;
        }

        // 스킬리스트에 있는 모든 스킬들 순환, 확인, 공격
        for (int i = 0; i < HeroEquipedSkill.Instance.EquipedSkillList.Count; i++)
        {
            Skill skill = HeroEquipedSkill.Instance.EquipedSkillList[i];

            if (skill.SkillUpdate())
            {
                skill.AttackFunc(transform.position);
            }
        }
    }

    // HP 감소
    public void HPMinus(int atk)
    {
        Hp -= atk;

        // 스프라이트 깜빡이기
        BlinkSprite blinkSprite = new BlinkSprite();
        StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));

        if (Hp < 0)
            Death();
    }

    // 죽음
    private void Death()
    {
        //Debug.Log("게임오버!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }


    //// 사거리 기즈모로 표시
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, _attackRange);
    //}  
}
