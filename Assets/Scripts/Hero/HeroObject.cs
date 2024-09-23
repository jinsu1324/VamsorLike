using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ���� ���ӿ�����Ʈ : �������� ������ / ���� ������ �̴� / ���� / �̵� / HP���� / ����
public class HeroObject : SerializedMonoBehaviour
{
    [Title("������ ��ü", bold: false)]
    // ���� ������Ʈ�� �� ������
    [SerializeField]
    private readonly HeroData _baseHeroData;

    // ������Ʈ�� �Ҵ�� ������
    public int Hp { get; set; }
    public int Atk { get; set; }
    public float Speed { get; set; }



    [Title("�ʿ��� ������Ʈ��", bold: false)]
    // ������ٵ�
    private Rigidbody2D _rigid;

    // ��������Ʈ ������
    private SpriteRenderer _spriteRenderer;

    // �̵��� ����� vector2 dir
    private Vector2 _moveDir;

    // HP��
    private HPBar _hpBar;


    private void FixedUpdate()
    {
        if (PlaySceneManager.Instance.IsGameStart)
        {
            Move();
            Attack();
        }            
    }

    // ������ ����
    public void DataSetting()
    {
        // ������ �־��ֱ�
        Hp = _baseHeroData.MaxHp;
        Atk = _baseHeroData.Atk;
        Speed = _baseHeroData.Speed;

        // �ʿ� ������Ʈ�� �����ͼ� �Ҵ�
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _moveDir = Vector2.zero;

        // HP�ٵ� �Ҵ�
        _hpBar = Instantiate(PlaySceneManager.Instance.HPBar, transform.position, Quaternion.identity);
        _hpBar.SetParent(this.transform);
        _hpBar.Update_HPSlider(Hp, _baseHeroData.MaxHp);
    }


    // �̵�
    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _moveDir.x = _rigid.position.x + (horizontal * Speed * Time.deltaTime);
        _moveDir.y = _rigid.position.y + (vertical * Speed * Time.deltaTime);

        _rigid.MovePosition(_moveDir);        
    }


    // ����
    private void Attack()
    {
        // ��ų����Ʈ�� �ƹ��͵� ������ �׳� ����
        if (SkillManager.Instance.HaveSkillList.Count == 0)
        {
            return;
        }

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

    // HP ����
    public void HPMinus(int atk)
    {
        Hp -= atk;

        // ��������Ʈ �����̱�
        BlinkSprite blinkSprite = new BlinkSprite();
        StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));

        _hpBar.Update_HPSlider(Hp, _baseHeroData.MaxHp);

        if (Hp < 0)
            Death();
    }

    // ����
    private void Death()
    {
        Debug.Log("���ӿ���!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
}
