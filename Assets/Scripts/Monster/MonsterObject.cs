using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class MonsterObject : SerializedMonoBehaviour
{
    [Title("������ ��ü", bold: false)]
    // ���� ������Ʈ�� �� ������
    [SerializeField]
    private readonly MonsterData _monsterData;

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

    // ��������Ʈ ������
    private SpriteRenderer _spriteRenderer;

    private void FixedUpdate()
    {
        // ���� ����ٴϵ���
        FollowHero();
    }

    // ������ ����
    public void DataSetting()
    {
        _name = _monsterData.Name;
        _hp = _monsterData.Hp;
        _atk = _monsterData.Atk;
        _speed = _monsterData.Speed;

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // HP ����
    public void HPMinus(int atk)
    {
        _hp -= atk;

        // ��������Ʈ �����̱�
        BlinkSprite blinkSprite = new BlinkSprite();
        StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));

        if (_hp < 0)
            Death();
    }


    // ����
    private void Death()
    {
        Destroy(this.gameObject);
    }


    // ���� �浹���� �� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {
            collision.gameObject.GetComponent<HeroObject>().HPMinus(_atk);
        }
    }


    // ���� ����ٴϵ���
    private void FollowHero()
    {
        this.gameObject.transform.position = 
            Vector3.Lerp(this.gameObject.transform.position, 
            PlaySceneManager.ThisGameHeroObject.transform.position,
            _speed * Time.deltaTime);
    }
}
