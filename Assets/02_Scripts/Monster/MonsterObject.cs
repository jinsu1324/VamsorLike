using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// ���� ���ӿ�����Ʈ : ���Ͱ��� ������ / ���� ������ �̴� / ���� / �÷��̾� ���� �̵� / HP���� / ����
/// </summary>
public class MonsterObject : ObjectPoolObject
{
    public static event Action<MonsterObject> OnMonsterDeath;   // ���� �׾����� ó���� �Լ��� �׼�

    [SerializeField]
    private readonly MonsterData _baseMonsterData;              // ���� ������Ʈ�� �� ������ 

    public float Hp { get; set; }                               // ������Ʈ HP
    public float Atk { get; set; }                              // ������Ʈ Atk
    public float Speed { get; set; }                            // ������Ʈ Speed

    private SpriteRenderer _spriteRenderer;                     // ��������Ʈ ������  
    private NavMeshAgent _navMeshAgent;                         // �׺�޽� ������Ʈ

    /// <summary>
    /// ������ ����
    /// </summary>
    public void DataSetting()
    {
        // ������ �־��ֱ�
        Hp = _baseMonsterData.MaxHp;
        Atk = _baseMonsterData.Atk;
        Speed = _baseMonsterData.Speed;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _navMeshAgent.speed = Speed;

        MonsterManager.Instance.AddFieldMonsterList(this);
    }

    /// <summary>
    /// HP ����
    /// </summary>
    public void HPMinus(float atk)
    {
        Hp -= atk;

        // ������ �ؽ�Ʈ UI �����ϰ� ����ֱ�
        TurnOn_DamageTextUI_fromPool(atk);

        // ��������Ʈ �����̱�
        BlinkSprite blinkSprite = new BlinkSprite();
        StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));

        if (Hp < 0)
            Death();
    }

    /// <summary>
    /// ���� ����ٴϵ���
    /// </summary>
    public void FollowHero()
    {
        _navMeshAgent.SetDestination(PlaySceneManager.ThisGameHeroObject.transform.position);

        //transform.position = Vector3.MoveTowards(
        //    transform.position,
        //    PlaySceneManager.ThisGameHeroObject.transform.position,
        //    Speed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// ���� (+ ���� �浹 ����)
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {
            collision.gameObject.GetComponent<HeroObject>().HPMinus(Atk);
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Death()
    {
        // ���� �׾����� �׼ǵ� ���� (�ʵ� ���� ����Ʈ���� �� ���� ���� / �ٴڿ� ����ġ ������ / �ٽ� ������Ʈ Ǯ�� ����������)
        OnMonsterDeath?.Invoke(this);

        Destroy(this.gameObject);
    }

    /// <summary>
    /// �������ؽ�ƮUI �����ϰ� ����ִ� �Լ�
    /// </summary>
    private void TurnOn_DamageTextUI_fromPool(float atk)
    {
        // ������ �ؽ�Ʈ ������Ʈ Ǯ���� ������ �����ϱ�
        GameObject go = PlaySceneManager.Instance.PlaySceneCanvas.DamageTextsPool.GetObj();
        DamageText damageTextUI = go.GetComponent<DamageText>();

        // ���� ������ ����
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        RectTransform rectTransform = damageTextUI.GetComponent<RectTransform>();
        rectTransform.position = screenPosition;

        // ������ �ؽ�Ʈ �ʱ�ȭ
        damageTextUI.Init(atk);
    }
}
