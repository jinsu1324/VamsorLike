using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

/// <summary>
/// ���� ���ӿ�����Ʈ
/// </summary>
public class HeroObj : SerializedMonoBehaviour
{
    [SerializeField]
    private HeroID _heroID;                         // �� ����������Ʈ�� ���� ID

    private HeroData _baseHeroData;                 // ���� ������Ʈ�� �� ������

    public float Hp { get; set; }                   // ������Ʈ HP
    public float Atk { get; set; }                  // ������Ʈ Atk
    public float Speed { get; set; }                // ������Ʈ Speed

    [SerializeField]
    private HPBar _hpBar;                           // HP��

    [SerializeField]
    private Canvas _heroCanvas;                     // ���� UI ĵ����

    [SerializeField]
    private GameObject _damagedEffect;              // ������ ���� �� ��Ÿ�� ����Ʈ

    private Rigidbody2D _rigid;                     // ������ٵ�
    private SpriteRenderer _spriteRenderer;         // ��������Ʈ ������
    private Animator _animator;                     // �ִϸ�����

    private Vector2 _moveDir;                       // �̵��� ����� vector2 dir    
    private float _lastHorizontalDirection = 1.0f;  // ���������� �ٶ� ���� (�⺻������ �������� �� ���·� ����)

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
    /// ������ ����
    /// </summary>
    public void DataSetting()
    {
        // �� ���� ID�� �ش��ϴ� ������ �Ҵ�
        _baseHeroData = DataManager.Instance.HeroDatas.GetDataById(_heroID.ToString());

        // ������ �־��ֱ�
        Hp = _baseHeroData.MaxHp;
        Atk = _baseHeroData.Atk;
        Speed = _baseHeroData.Speed;

        // �ʿ� ������Ʈ�� �����ͼ� �Ҵ�
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _moveDir = Vector2.zero;

        // ī�޶� ����
        _heroCanvas.worldCamera = Camera.main;

        // HP �� ������Ʈ
        _hpBar.Update_HPSlider(Hp, _baseHeroData.MaxHp);

        // ������ ����Ʈ ������
        DeactivateDamageEffect();

        SkillID startSkillID = (SkillID)Enum.Parse(typeof(SkillID), _baseHeroData.StartSkill); 

        PlaySceneManager.Instance.SkillManager.AddSkill(startSkillID);

    }

    /// <summary>
    /// �̵�
    /// </summary>
    public void Move()
    {
        float horizontal = PlaySceneCanvas.Instance.Joystick.Horizontal;
        float vertical = PlaySceneCanvas.Instance.Joystick.Vertical;

        _moveDir.x = _rigid.position.x + (horizontal * Speed * Time.deltaTime);
        _moveDir.y = _rigid.position.y + (vertical * Speed * Time.deltaTime);

        _rigid.MovePosition(_moveDir);

        // �¿� �����ӿ� ���� ��������Ʈ �ø�
        SpriteFlip(horizontal);

        // �ִϸ��̼� ���� (�����Ӱ��� ���� isMove�� true false)
        _animator.SetBool("isMove", horizontal != 0 || vertical != 0);
    }

    /// <summary>
    /// �¿� �����ӿ� ���� ��������Ʈ �ø�
    /// </summary>
    private void SpriteFlip(float horizontal)
    {
        // horizontal�� 0�� �ƴ� ���� ������ ������Ʈ
        if (horizontal != 0)
        {
            // ������ ���� ����
            _lastHorizontalDirection = horizontal;

            // horizontal�� 0���� ������ flipX�� true��, 0���� ũ�� flipX�� false��
            _spriteRenderer.flipX = horizontal < 0; 
        }
        else
        {
            // ������ �� _lastHorizontalDirection�� 0���� ������ ������, �ƴϸ� �״�� (flip�� true false)
            _spriteRenderer.flipX = _lastHorizontalDirection < 0;
        }
    }

    /// <summary>
    /// ���� �Լ�
    /// </summary>
    private void Attack()
    {
        // ��ų����Ʈ�� �ƹ��͵� ������ �׳� ����
        if (PlaySceneManager.Instance.SkillManager.HaveSkillList.Count == 0)
            return;

        // ��ų����Ʈ�� �ִ� ��� ��ų�� ��ȯ, Ȯ��, ����
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
    /// HP ����
    /// </summary>
    public void HPMinus(float atk)
    {
        Hp -= atk;

        Debug.Log($"���� HP : {Hp} / {_baseHeroData.MaxHp}");

        // ��������Ʈ �����̱�
        //BlinkSprite blinkSprite = new BlinkSprite();
        //StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));

        _hpBar.Update_HPSlider(Hp, _baseHeroData.MaxHp);

        if (Hp <= 0)
            Death();
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Death()
    {
        Debug.Log("���ӿ���");
        _animator.SetTrigger("Dead");

        
        PlaySceneCanvas.Instance.ResultPopup.OpenPopup();
    }

    /// <summary>
    /// �´� ������ ����Ʈ Ȱ��ȭ
    /// </summary>
    public void ActivateDamagedEffect()
    {
        _damagedEffect.SetActive(true);
        _spriteRenderer.color = Color.red;
    }

    /// <summary>
    /// ������ ����Ʈ ��Ȱ��ȭ
    /// </summary>
    public void DeactivateDamageEffect()
    {
        _damagedEffect.SetActive(false);
        _spriteRenderer.color = Color.white;
    }

    /// <summary>
    /// �� ������ ȹ�� �� ȿ��
    /// </summary>
    public void Heal(int amount)
    {
        if (Hp <= 0)
            return;

        Hp += amount;

        if (Hp >= _baseHeroData.MaxHp)
            Hp = _baseHeroData.MaxHp;

        Debug.Log($"���� HP : {Hp} / {_baseHeroData.MaxHp}");

        _hpBar.Update_HPSlider(Hp, _baseHeroData.MaxHp);
    }

    /// <summary>
    /// EXP ������ ȹ�� (EXP Manager�� EXPUP ��û)
    /// </summary>
    public void AcquireExp_and_Request(int amount)
    {
        PlaySceneManager.Instance.LevelManager.EXPUp(amount);
    }

    /// <summary>
    /// ��� ������ ȹ�� (Gold Manager�� GoldUP ��û)
    /// </summary>
    public void AcquireGold_and_Request(int amount)
    {
        PlaySceneCanvas.Instance.PlayAchivementUI.AddGold(amount);
    }

    /// <summary>
    /// ������� ������ ȹ�� (Gold Manager�� GoldUP ��û)
    /// </summary>
    public void AcquireRewardBox_and_Request()
    {
        PlaySceneCanvas.Instance.RewardBoxPopup.Initialize_Popup();
    }

    /// <summary>
    /// �ڼ� ������ ȹ�� (ItemManager�� ��û)
    /// </summary>
    public void AcquireMagnet_and_Request()
    {
        PlaySceneManager.Instance.ItemManager.ActivateMagnet();
    }
}
