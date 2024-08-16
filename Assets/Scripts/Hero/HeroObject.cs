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
    private string _name;
    public string Name { get { return _name; } set { _name = value; } }

    // ü��
    [SerializeField]
    private int _hp;
    public int Hp { get { return _hp; } set { _hp = value; } }

    // ���ݷ�
    [SerializeField]
    private int _atk;
    public int Atk { get { return _atk; } set { _atk = value; } }

    // �̵��ӵ�
    [SerializeField]
    private float _speed;
    public float Speed { get { return _speed; } set { _speed = value; } }

    // ��Ÿ�
    [SerializeField]
    private float _range;
    public float Range { get { return _range; } set { _range = value; } }

    // ���� ������
    [SerializeField]
    private float _delay;
    public float Delay { get { return _delay; } set { _delay = value; } }

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

    [SerializeField]
    private float _attackRange;

    float time = 0;

    private List<HeroSkillBase> _skillList = new List<HeroSkillBase>();

    private void FixedUpdate()
    {
        // �̵�
        Move();

        Attack();

    }

    // ������ ����
    public void DataSetting()
    {
        // ������ �־��ֱ�
        _name = _heroData.Name;
        _hp = _heroData.Hp;
        _atk = _heroData.Atk;
        _speed = _heroData.Speed;
        _range = _heroData.Range;
        _delay = _heroData.Delay;
        _attackRangeCollider.size = _attackRangeCollider.size * _range;

        // �ʿ� ������Ʈ�� �����ͼ� �Ҵ�
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _moveDir = Vector2.zero;


        HeroSKillSword heroSkillSword = new HeroSKillSword(_atk, _range, _delay);
        _skillList.Add(heroSkillSword);

        HeroSKillBow heroSKillBow = new HeroSKillBow(_atk, _range, _delay);
        _skillList.Add(heroSKillBow);
    }

    // ���� ����
    public void AttackStart()
    {
        //StartCoroutine(Attack());
    }


    // �̵�
    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _moveDir.x = _rigid.position.x + (horizontal * _speed * Time.deltaTime);
        _moveDir.y = _rigid.position.y + (vertical * _speed * Time.deltaTime);

        _rigid.MovePosition(_moveDir);        
    }


    // ���� �ڷ�ƾ
    private void Attack()
    {        
        // ���� ���۵Ǹ� �ݺ�
        if (PlaySceneManager.IsGameStart)
        {

            for (int i = 0; i < _skillList.Count; i++)
            {
                HeroSkillBase skill = _skillList[i];

                if (skill.SkillUpdate())
                {
                    skill.AttackFunc(transform.position);
                }


            }
        }
    }

    IEnumerator coroutine;
    // HP ����
    public void HPMinus(int atk)
    {
        _hp -= atk;

        // ��������Ʈ �����̱�

        if(coroutine != null)
            StopCoroutine(coroutine);

        coroutine = BlinkSprite.Blink(_spriteRenderer, 0.1f);
        StartCoroutine(coroutine);

        if (_hp < 0)
            Death();
    }

    // ����
    private void Death()
    {
        Debug.Log("���ӿ���!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }







    // ��Ÿ� ������ ǥ��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _attackRange);
    }  
}
