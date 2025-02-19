using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 영웅 게임오브젝트
/// </summary>
public class HeroObj : SerializedMonoBehaviour
{
    [SerializeField]
    private HeroID _heroID;                         // 이 영웅오브젝트의 영웅 ID

    private HeroData _baseHeroData;                 // 영웅 오브젝트에 들어갈 데이터

    public float Hp { get; set; }                   // 오브젝트 HP
    public float Atk { get; set; }                  // 오브젝트 Atk
    public float Speed { get; set; }                // 오브젝트 Speed

    [SerializeField]
    private HPBar _hpBar;                           // HP바

    [SerializeField]
    private Canvas _heroCanvas;                     // 영웅 UI 캔버스

    [SerializeField]
    private GameObject _damagedEffect;              // 데미지 받을 때 나타날 이펙트

    private Rigidbody2D _rigid;                     // 리지드바디
    private SpriteRenderer _spriteRenderer;         // 스프라이트 렌더러
    private Animator _animator;                     // 애니메이터

    private Vector2 _moveDir;                       // 이동에 사용할 vector2 dir    
    private float _lastHorizontalDirection = 1.0f;  // 마지막으로 바라본 방향 (기본적으로 오른쪽을 본 상태로 시작)

    private float _cautionLimitHP;                  // 위험표시 할 리미트 HP

    /// <summary>
    /// FixedUpdate
    /// </summary>
    private void FixedUpdate()
    {
        if (PlaySceneManager.Instance.IsGameStart == false)
            return;

        Move();
        Attack();
    }

    /// <summary>
    /// 데이터 셋팅
    /// </summary>
    public void DataSetting()
    {
        // 이 영웅 ID에 해당하는 데이터 할당
        _baseHeroData = DataManager.Instance.HeroDatas.GetDataById(_heroID.ToString());

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
        _cautionLimitHP = _baseHeroData.MaxHp / 4;

        // 데미지 이펙트 꺼놓기
        DeactivateDamageEffect();

        SkillID startSkillID = (SkillID)Enum.Parse(typeof(SkillID), _baseHeroData.StartSkill); 

        PlaySceneManager.Instance.SkillManager.AddSkill(startSkillID);

    }

    /// <summary>
    /// 이동
    /// </summary>
    public void Move()
    {
        float horizontal = PlaySceneCanvas.Instance.Joystick.Horizontal;
        float vertical = PlaySceneCanvas.Instance.Joystick.Vertical;

        _moveDir.x = _rigid.position.x + (horizontal * Speed * Time.deltaTime);
        _moveDir.y = _rigid.position.y + (vertical * Speed * Time.deltaTime);

        _rigid.MovePosition(_moveDir);

        // 좌우 움직임에 따라 스프라이트 플립
        SpriteFlip(horizontal);

        // 애니메이션 설정 (움직임값에 따라서 isMove를 true false)
        _animator.SetBool("isMove", horizontal != 0 || vertical != 0);
    }

    /// <summary>
    /// 좌우 움직임에 따라 스프라이트 플립
    /// </summary>
    private void SpriteFlip(float horizontal)
    {
        // horizontal이 0이 아닐 때만 방향을 업데이트
        if (horizontal != 0)
        {
            // 마지막 방향 저장
            _lastHorizontalDirection = horizontal;

            // horizontal이 0보다 작으면 flipX를 true로, 0보다 크면 flipX를 false로
            _spriteRenderer.flipX = horizontal < 0; 
        }
        else
        {
            // 멈췄을 때 _lastHorizontalDirection이 0보다 작으면 뒤집고, 아니면 그대로 (flip을 true false)
            _spriteRenderer.flipX = _lastHorizontalDirection < 0;
        }
    }

    /// <summary>
    /// 공격 함수
    /// </summary>
    private void Attack()
    {
        // 스킬리스트에 아무것도 없으면 그냥 리턴
        if (PlaySceneManager.Instance.SkillManager.HaveSkillList.Count == 0)
            return;

        // 스킬리스트에 있는 모든 스킬들 순환, 확인, 공격
        for (int i = 0; i < PlaySceneManager.Instance.SkillManager.HaveSkillList.Count; i++)
        {
            Skill_Base skill = PlaySceneManager.Instance.SkillManager.HaveSkillList[i];

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

        //Debug.Log($"현재 HP : {Hp} / {_baseHeroData.MaxHp}");
        _hpBar.Update_HPSlider(Hp, _baseHeroData.MaxHp);

        // 오디오 재생
        //AudioManager.Instance.PlaySFX(SFXType.HeroDamaged);

        CheckCautionViewOnOFF();

        if (Hp <= 0)
            Death();
    }

    /// <summary>
    /// 화면 붉은색 위험표시 ON OFF
    /// </summary>
    private void CheckCautionViewOnOFF()
    {
        if (Hp < _cautionLimitHP)
            PlaySceneCanvas.Instance.CautionView.OpenCautionView();
        else
            PlaySceneCanvas.Instance.CautionView.CloseCautionView();
    }

    /// <summary>
    /// 죽음
    /// </summary>
    public void Death()
    {
        PlaySceneManager.Instance.IsGameStartChange(false);
        
        _animator.SetTrigger("Dead");

        // 오디오 재생
        AudioManager.Instance.PlaySFX(SFXType.GameOver);
    }

    /// <summary>
    /// 죽음 애니메이션 후 처리할 것들
    /// </summary>
    public void AfterDeathAnimTask()
    {
        PlaySceneCanvas.Instance.CautionView.CloseCautionView();
        PlaySceneCanvas.Instance.ResultPopup.OpenPopup();
    }

    /// <summary>
    /// 맞는 데미지 이펙트 활성화
    /// </summary>
    public void ActivateDamagedEffect()
    {
        _damagedEffect.SetActive(true);
        _spriteRenderer.color = Color.red;
    }

    /// <summary>
    /// 데미지 이펙트 비활성화
    /// </summary>
    public void DeactivateDamageEffect()
    {
        _damagedEffect.SetActive(false);
        _spriteRenderer.color = Color.white;
    }

    /// <summary>
    /// 힐 아이템 획득 시 효과
    /// </summary>
    public void Heal(int amount)
    {
        if (Hp <= 0)
            return;

        Hp += amount;
        PlaySceneManager.Instance.EffectManager.GetEffect(EffectName.FX_Heal.ToString(), this.transform.position);

        if (Hp >= _baseHeroData.MaxHp)
            Hp = _baseHeroData.MaxHp;

        CheckCautionViewOnOFF();
        //Debug.Log($"현재 HP : {Hp} / {_baseHeroData.MaxHp}");

        _hpBar.Update_HPSlider(Hp, _baseHeroData.MaxHp);
    }

    /// <summary>
    /// EXP 아이템 획득 (EXP Manager에 EXPUP 요청)
    /// </summary>
    public void AcquireExp_and_Request(int amount)
    {
        PlaySceneManager.Instance.LevelManager.EXPUp(amount);
    }

    /// <summary>
    /// 골드 아이템 획득 (Gold Manager에 GoldUP 요청)
    /// </summary>
    public void AcquireGold_and_Request(int amount)
    {
        PlaySceneCanvas.Instance.PlayAchivementUI.AddGold(amount);
    }

    /// <summary>
    /// 보상상자 아이템 획득 (Gold Manager에 GoldUP 요청)
    /// </summary>
    public void AcquireRewardBox_and_Request()
    {
        PlaySceneCanvas.Instance.RewardBoxPopup.Initialize();
    }

    /// <summary>
    /// 자석 아이템 획득 (ItemManager에 요청)
    /// </summary>
    public void AcquireMagnet_and_Request()
    {
        PlaySceneManager.Instance.ItemManager.ActivateMagnet();
    }
}
