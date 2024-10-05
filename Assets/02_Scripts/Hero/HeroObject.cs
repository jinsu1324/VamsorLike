using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
        float horizontal = PlaySceneManager.Instance.JoystickUI.Horizontal;
        float vertical = PlaySceneManager.Instance.JoystickUI.Vertical;

        _moveDir.x = _rigid.position.x + (horizontal * Speed * Time.deltaTime);
        _moveDir.y = _rigid.position.y + (vertical * Speed * Time.deltaTime);

        _rigid.MovePosition(_moveDir);

        // �ִϸ��̼� ���� (�����Ӱ��� ���� isMove�� true false)
        _animator.SetBool("isMove", horizontal != 0 || vertical != 0);
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
    private void Death()
    {
        Debug.Log("���ӿ���!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
}
