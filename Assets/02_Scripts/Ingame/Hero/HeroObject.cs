using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;


/// <summary>
/// ���� ���ӿ�����Ʈ : �������� ������ / ���� ������ �̴� / ���� / �̵� / HP���� / ����
/// </summary>
public class HeroObject : SerializedMonoBehaviour
{
    [SerializeField]
    private readonly HeroData _baseHeroData;        // ���� ������Ʈ�� �� ������

    public float Hp { get; set; }                   // ������Ʈ HP
    public float Atk { get; set; }                  // ������Ʈ Atk
    public float Speed { get; set; }                // ������Ʈ Speed

    [SerializeField]
    private HPBar _hpBar;                           // HP��

    [SerializeField]
    private Canvas _heroCanvas;                     // ���� UI ĵ����

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
    }

    /// <summary>
    /// �̵�
    /// </summary>
    public void Move()
    {
        float horizontal = PlaySceneManager.Instance.PlaySceneCanvas.Joystick.Horizontal;
        float vertical = PlaySceneManager.Instance.PlaySceneCanvas.Joystick.Vertical;

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
        if (SkillManager.Instance.HaveSkillList.Count == 0)
            return;

        // ��ų����Ʈ�� �ִ� ��� ��ų�� ��ȯ, Ȯ��, ����
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
    /// HP ����
    /// </summary>
    public void HPMinus(float atk)
    {
        Hp -= atk;

        Debug.Log("���� HP : " + Hp);

        // ��������Ʈ �����̱�
        BlinkSprite blinkSprite = new BlinkSprite();
        StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));

        _hpBar.Update_HPSlider(Hp, _baseHeroData.MaxHp);

        if (Hp < 0)
            Death();
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Death()
    {
        Debug.Log("���ӿ���");
        _animator.SetTrigger("Dead");

        PlaySceneManager.Instance.IsGameStartChange(false);
        PlaySceneManager.Instance.PlaySceneCanvas.ResultPopup.OpenPopup();
    }
}
