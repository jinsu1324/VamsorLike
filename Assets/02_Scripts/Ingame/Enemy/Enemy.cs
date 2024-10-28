using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : ObjectPoolObject
{
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
    /// ������ ���
    /// </summary>
    public abstract void DropItem();

    /// <summary>
    /// ����
    /// </summary>
    public virtual void Death()
    {
        // �ʵ� �� ��Ͽ��� ����
        PlaySceneManager.Instance.EnemyManager.RemoveFieldEnemyList(this);

        // �ٽ� ������Ʈ Ǯ�� ����������
        PlaySceneManager.Instance.EnemySpawner.EnemyBackTrans(this);

        // ��Ż �� ����Ƚ�� ����
        PlaySceneManager.Instance.AchivementManager.AddKillCount();
        
        // ������ ���
        DropItem();
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
    public virtual void HPMinus(float atk)
    {
        _hp -= atk;

        // ������ �ؽ�Ʈ UI �����ϰ� ����ֱ�
        TurnOn_DamageTextUI_fromPool(atk);

        // ��������Ʈ �����̱�
        BlinkSprite blinkSprite = new BlinkSprite();
        StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));        
    }

    /// <summary>
    /// �������ؽ�ƮUI �����ϰ� ����ִ� �Լ�
    /// </summary>
    public void TurnOn_DamageTextUI_fromPool(float atk)
    {
        // ������ �ؽ�Ʈ ������Ʈ Ǯ���� ������ �����ϱ�
        GameObject go = PlaySceneCanvas.Instance.DamageTextsPool.GetObj();
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
