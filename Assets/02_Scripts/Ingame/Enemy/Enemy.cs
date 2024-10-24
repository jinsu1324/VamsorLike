using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : ObjectPoolObject
{
    public static event Action<Enemy> OnEnemyDead;   // �� �׾����� ó���� delegate

    protected float _hp;                              // ������Ʈ HP
    protected float _atk;                             // ������Ʈ Atk
    protected float _speed;                           // ������Ʈ Speed

    protected SpriteRenderer _spriteRenderer;         // ��������Ʈ ������  
    protected NavMeshAgent _navMeshAgent;             // �׺�޽� ������Ʈ


    /// <summary>
    /// ������ ����
    /// </summary>
    public abstract void DataSetting();

    /// <summary>
    /// ����
    /// </summary>
    public virtual void Death()
    {
        OnEnemyDead?.Invoke(this);
    }    

    /// <summary>
    /// ���� (���� �浹 ������ ����)
    /// </summary>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {
            collision.gameObject.GetComponent<HeroObj>().HPMinus(_atk);
        }
    }

    /// <summary>
    /// HP ����
    /// </summary>
    public void HPMinus(float atk)
    {
        _hp -= atk;

        // ������ �ؽ�Ʈ UI �����ϰ� ����ֱ�
        TurnOn_DamageTextUI_fromPool(atk);

        // ��������Ʈ �����̱�
        BlinkSprite blinkSprite = new BlinkSprite();
        StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));

        // HP 0 ���ϸ� ����
        if (_hp < 0)
            Death();
    }

    /// <summary>
    /// �������ؽ�ƮUI �����ϰ� ����ִ� �Լ�
    /// </summary>
    public void TurnOn_DamageTextUI_fromPool(float atk)
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

    /// <summary>
    /// ���� ����ٴϵ���
    /// </summary>
    public void FollowHero()
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(PlaySceneManager.Instance.MyHeroObj.transform.position);
    }

    /// <summary>
    /// ����ٴϴ°� ���߱�
    /// </summary>
    public void Stop()
    {
        _navMeshAgent.isStopped = true;
    }
}
