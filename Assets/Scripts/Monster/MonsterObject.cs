using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class MonsterObject : SerializedMonoBehaviour
{
    // ���� ������ ��ũ���ͺ� ������Ʈ
    [SerializeField]
    private readonly MonsterData _monsterData;

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


    private SpriteRenderer _spriteRenderer;

    // ����
    public void Spawn()
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
        StartCoroutine(Blink());

        if (_hp < 0)
            Death();
    }

    // �ǰݽ� �����̱�
    private IEnumerator Blink()
    {
        _spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _spriteRenderer.color = Color.white;
    }

    // ����
    private void Death()
    {
        Destroy(this.gameObject);
    }


    private void Update()
    {
        FollowHero();
    }


    // ���� ����ٴϵ���
    private void FollowHero()
    {
        this.gameObject.transform.position = 
            Vector3.Lerp(this.gameObject.transform.position, 
            PlaySceneManager.ThisGameHeroObject.transform.position,
            _speed * Time.deltaTime);
    }

    // ���� �浹���� �� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            collision.gameObject.GetComponent<HeroObject>().HPMinus(_atk);
        }        
    }

}
