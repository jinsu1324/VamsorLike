using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 영웅 게임오브젝트 : 영웅관련 데이터 / 본인 데이터 이닛 / 공격 / 이동 / HP감소 / 죽음
public class HeroObject : SerializedMonoBehaviour
{
    [Title("데이터 본체", bold: false)]
    // 영웅 오브젝트에 들어갈 데이터
    [SerializeField]
    private readonly HeroData _baseHeroData;

    // 오브젝트에 할당될 데이터
    public int Hp { get; set; }
    public int Atk { get; set; }
    public float Speed { get; set; }



    [Title("필요한 컴포넌트들", bold: false)]
    // 리지드바디
    private Rigidbody2D _rigid;

    // 스프라이트 렌더러
    private SpriteRenderer _spriteRenderer;

    // 이동에 사용할 vector2 dir
    private Vector2 _moveDir;

    // HP바
    private HPBar _hpBar;


    private void FixedUpdate()
    {
        if (PlaySceneManager.Instance.IsGameStart)
        {
            Move();
            Attack();
        }            
    }

    // 데이터 셋팅
    public void DataSetting()
    {
        // 데이터 넣어주기
        Hp = _baseHeroData.MaxHp;
        Atk = _baseHeroData.Atk;
        Speed = _baseHeroData.Speed;

        // 필요 컴포넌트들 가져와서 할당
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _moveDir = Vector2.zero;

        // HP바도 할당
        _hpBar = Instantiate(PlaySceneManager.Instance.HPBar, transform.position, Quaternion.identity);
        _hpBar.SetParent(this.transform);
        _hpBar.Update_HPSlider(Hp, _baseHeroData.MaxHp);
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
        if (SkillManager.Instance.HaveSkillList.Count == 0)
        {
            return;
        }

        // 스킬리스트에 있는 모든 스킬들 순환, 확인, 공격
        for (int i = 0; i < SkillManager.Instance.HaveSkillList.Count; i++)
        {
            Skill_Base skill = SkillManager.Instance.HaveSkillList[i];

            if (skill.SkillCooltime())
            {
                SkillAttackArgs skillAttackArgs = new SkillAttackArgs() { StartSkillPos = transform.position };
                skill.UseSkill(skillAttackArgs);
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

        _hpBar.Update_HPSlider(Hp, _baseHeroData.MaxHp);

        if (Hp < 0)
            Death();
    }

    // 죽음
    private void Death()
    {
        Debug.Log("게임오버!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
}
