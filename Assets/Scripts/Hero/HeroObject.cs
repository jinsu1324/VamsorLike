using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // ���ݹ��� �ݶ��̴�
    [SerializeField]
    private BoxCollider2D _attackRangeCollider;

    // ������ٵ�
    private Rigidbody2D _rigid;

    // ��������Ʈ ������
    private SpriteRenderer _spriteRenderer;

    // �̵��� ����� vector2 dir
    private Vector2 _moveDir;


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
        _attackRangeCollider.size = _attackRangeCollider.size * Range;

        // �ʿ� ������Ʈ�� �����ͼ� �Ҵ�
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _moveDir = Vector2.zero;
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
            Debug.Log("EquipedSkillList Count 0");
            return;
        }

        // ��ų����Ʈ�� �ִ� ��� ��ų�� ��ȯ, Ȯ��, ����
        for (int i = 0; i < HeroEquipedSkill.Instance.EquipedSkillList.Count; i++)
        {
            Skill skill = HeroEquipedSkill.Instance.EquipedSkillList[i];

            if (skill.SkillUpdate())
            {
                skill.AttackFunc(transform.position);
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

        if (Hp < 0)
            Death();
    }

    // ����
    private void Death()
    {
        //Debug.Log("���ӿ���!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }


    //// ��Ÿ� ������ ǥ��
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, _attackRange);
    //}  
}
