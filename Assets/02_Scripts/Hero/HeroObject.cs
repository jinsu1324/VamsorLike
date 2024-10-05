using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 영웅 게임오브젝트 : 영웅관련 데이터 / 본인 데이터 이닛 / 공격 / 이동 / HP감소 / 죽음
/// </summary>
public class HeroObject : SerializedMonoBehaviour
{
    [SerializeField]
    private readonly HeroData _baseHeroData;        // 영웅 오브젝트에 들어갈 데이터

    public float Hp { get; set; }                   // 오브젝트 HP
    public float Atk { get; set; }                  // 오브젝트 Atk
    public float Speed { get; set; }                // 오브젝트 Speed

    [SerializeField]
    private HPBar _hpBar;                           // HP바

    [SerializeField]
    private Canvas _heroCanvas;                     // 영웅 UI 캔버스

    private Rigidbody2D _rigid;                     // 리지드바디
    private SpriteRenderer _spriteRenderer;         // 스프라이트 렌더러
    private Animator _animator;                     // 애니메이터

    private Vector2 _moveDir;                       // 이동에 사용할 vector2 dir    

    /// <summary>
    /// FixedUpdate
    /// </summary>
    private void FixedUpdate()
    {
        if (PlaySceneManager.Instance.IsGameStart)
        {
            Move();
            Attack();
        }            
    }

    /// <summary>
    /// 데이터 셋팅
    /// </summary>
    public void DataSetting()
    {
        // 데이터 넣어주기
        Hp = _baseHeroData.MaxHp;
        Atk = _baseHeroData.Atk;
        Speed = _baseHeroData.Speed;

        // 필요 컴포넌트들 가져와서 할당
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _moveDir = Vector2.zero;

        // 카메라 지정
        _heroCanvas.worldCamera = Camera.main;

        // HP 바 업데이트
        _hpBar.Update_HPSlider(Hp, _baseHeroData.MaxHp);
    }

    /// <summary>
    /// 이동
    /// </summary>
    public void Move()
    {
        float horizontal = PlaySceneManager.Instance.JoystickUI.Horizontal;
        float vertical = PlaySceneManager.Instance.JoystickUI.Vertical;

        _moveDir.x = _rigid.position.x + (horizontal * Speed * Time.deltaTime);
        _moveDir.y = _rigid.position.y + (vertical * Speed * Time.deltaTime);

        _rigid.MovePosition(_moveDir);

        // 애니메이션 설정 (움직임값에 따라서 isMove를 true false)
        _animator.SetBool("isMove", horizontal != 0 || vertical != 0);
    }

    /// <summary>
    /// 공격 함수
    /// </summary>
    private void Attack()
    {
        // 스킬리스트에 아무것도 없으면 그냥 리턴
        if (SkillManager.Instance.HaveSkillList.Count == 0)
            return;

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

    /// <summary>
    /// HP 감소
    /// </summary>
    public void HPMinus(float atk)
    {
        Hp -= atk;

        // 스프라이트 깜빡이기
        BlinkSprite blinkSprite = new BlinkSprite();
        StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));

        _hpBar.Update_HPSlider(Hp, _baseHeroData.MaxHp);

        if (Hp < 0)
            Death();
    }

    /// <summary>
    /// 죽음
    /// </summary>
    private void Death()
    {
        Debug.Log("게임오버!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
}
