using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

// ���� ���ӿ�����Ʈ : ���Ͱ��� ������ / ���� ������ �̴� / ���� / �÷��̾� ���� �̵� / HP���� / ����
public class MonsterObject : ObjectPoolObject
{
    [Title("������ ��ü", bold: false)]
    // ���� ������Ʈ�� �� ������
    [SerializeField]
    private readonly MonsterData _baseMonsterData;

    // ������Ʈ�� �Ҵ�� ������
    public int Hp { get; set; }
    public int Atk { get; set; }
    public float Speed { get; set; }

    // ��������Ʈ ������
    private SpriteRenderer _spriteRenderer;

    // ���� �׾����� ó���� �Լ��� �׼�
    public static event Action<MonsterObject> OnMonsterDeath;


    // ������ ����
    public void DataSetting()
    {
        // ������ �Ҵ�
        Hp = _baseMonsterData.MaxHp;
        Atk = _baseMonsterData.Atk;
        Speed = _baseMonsterData.Speed;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        MonsterManager.Instance.AddFieldMonsterList(this);
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
        // ���� �׾����� �׼ǵ� ���� (�ʵ� ���� ����Ʈ���� �� ���� ���� / �ٴڿ� ����ġ ������ / �ٽ� ������Ʈ Ǯ�� ����������)
        OnMonsterDeath?.Invoke(this);


        Destroy(this.gameObject);
    }


    // ���� �浹���� �� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {
            collision.gameObject.GetComponent<HeroObject>().HPMinus(Atk);
        }
    }


    // ���� ����ٴϵ���
    public void FollowHero()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            PlaySceneManager.ThisGameHeroObject.transform.position,
            Speed * Time.fixedDeltaTime);
    }
}
