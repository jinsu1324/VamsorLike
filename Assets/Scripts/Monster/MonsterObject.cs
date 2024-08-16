using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

// ���� ���ӿ�����Ʈ : ���Ͱ��� ������ / ���� ������ �̴� / ���� / �÷��̾� ���� �̵� / HP���� / ����
public class MonsterObject : SerializedMonoBehaviour
{
    [Title("������ ��ü", bold: false)]
    // ���� ������Ʈ�� �� ������
    [SerializeField]
    private readonly MonsterData _monsterData;

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
        Name = _monsterData.Name;
        Hp = _monsterData.Hp;
        Atk = _monsterData.Atk;
        Speed = _monsterData.Speed;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        SceneSingleton<MonsterManager>.Instance.SpawnMonster(this);
    }

    // HP ����
    public void HPMinus(int atk)
    {
        Hp -= atk;

        // ��������Ʈ �����̱�
        //BlinkSprite blinkSprite = new BlinkSprite();
        //StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));

        if (Hp < 0)
            Death();
    }


    // ����
    private void Death()
    {
        SceneSingleton<MonsterManager>.Instance.DieMonster(this);
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
    private void FollowHero()
    {
        this.gameObject.transform.position = 
            Vector3.Lerp(this.gameObject.transform.position, 
            PlaySceneManager.ThisGameHeroObject.transform.position,
            Speed * Time.deltaTime);
    }
}
