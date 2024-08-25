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
    private readonly HeroData _heroData;

    [Title("�Ҵ�� ������ ����", bold: false)]
    // �̸�
    [SerializeField]
    public string Name { get; set; }

    // ü��
    [SerializeField]
    public int Hp { get; set; }

    // ���ݷ�
    [SerializeField]
    public int Atk { get; set; }

    // �̵��ӵ�
    [SerializeField]
    public float Speed { get; set; }

    // ��Ÿ�
    [SerializeField]
    public float Range { get; set; }

    // ���� ������
    [SerializeField]
    public float Delay { get; set; }


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
        if (PlaySceneManager.IsGameStart)
        {
            Move();
            Attack();
        }            
    }

    // ������ ����
    public void DataSetting()
    {
        // ������ �־��ֱ�
        Name = _heroData.Name;
        Hp = _heroData.Hp;
        Atk = _heroData.Atk;
        Speed = _heroData.Speed;
        Range = _heroData.Range;
        Delay = _heroData.Delay;

        // �ʿ� ������Ʈ�� �����ͼ� �Ҵ�
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _moveDir = Vector2.zero;

        // HP�ٵ� �Ҵ�
        _hpBar = Instantiate(PlaySceneManager.Instance.HPBar, transform.position, Quaternion.identity);
        _hpBar.SetParent(this.transform);
        _hpBar.GetComponent<Slider>().value = Hp / _heroData.Hp;
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
        if (HeroEquipedSkill.Instance.EquipedSkillList.Count == 0)
        {
            return;
        }

        // ��ų����Ʈ�� �ִ� ��� ��ų�� ��ȯ, Ȯ��, ����
        for (int i = 0; i < HeroEquipedSkill.Instance.EquipedSkillList.Count; i++)
        {
            SkillBase skill = HeroEquipedSkill.Instance.EquipedSkillList[i];

            if (skill.SkillUpdate())
            {
                SkillAttackArgs skillAttackArgs = new SkillAttackArgs() { StartSkillPos = transform.position };
                skill.AttackFunc(skillAttackArgs);
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

        _hpBar.GetComponent<Slider>().value = (float)Hp / (float)_heroData.Hp;

        if (Hp < 0)
            Death();
    }

    // ����
    private void Death()
    {
        //Debug.Log("���ӿ���!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
}
