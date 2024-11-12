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

    protected Animator _animator;                     // �ִϸ�����
    protected SpriteRenderer _spriteRenderer;         // ��������Ʈ ������  
    protected BoxCollider2D _boxCollider2D;           // �ݶ��̴�

    protected bool _isDead = false;                   // ���� �׾����� 
    public bool IsDead => _isDead;                    // ���� �׾����� ���� �ܺο��� Ȯ�θ� �����ϰ� ������Ƽ

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
        // �׾����� true ��
        _isDead = true;

        // �ִϸ��̼� ���
        _animator.SetTrigger("isDead");
        
        // ������ ���
        DropItem();

        // �� ����Ƚ�� ����
        PlaySceneCanvas.Instance.PlayAchivementUI.AddKillCount();

        // �ʵ� �� ��Ͽ��� ����
        PlaySceneManager.Instance.EnemyManager.RemoveFieldEnemyList(this);

    }

    /// <summary>
    /// ���� �ִϸ��̼� �� ó���� �͵�
    /// </summary>
    public void AfterDeathAnimTask()
    {
        // �ٽ� ������Ʈ Ǯ�� ����������
        PlaySceneManager.Instance.EnemySpawner.EnemyBackTrans(this);
    }

    /// <summary>
    /// ������ ��� ������ ��� ����
    /// </summary>
    public void OnCollisionStay2D(Collision2D collision)
    {
        // �̹� �׾����� �ƹ��͵� �����ʰ� ����
        if (_isDead)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer(Layer.Hero.ToString()))
        {
            HeroObj hero = collision.gameObject.GetComponent<HeroObj>();

            if (hero == null)
                return;

            hero.HPMinus(_atk);
            hero.ActivateDamagedEffect();
        }
    }

    /// <summary>
    /// �������� ����� ���� ������ ����Ʈ ����
    /// </summary>
    public void OnCollisionExit2D(Collision2D collision)
    {
        HeroObj hero = collision.gameObject.GetComponent<HeroObj>();

        if (hero == null)
            return;
        
        hero.DeactivateDamageEffect();
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
        DamageText damageText = go.GetComponent<DamageText>();

        // ������ �ؽ�Ʈ ��ġ ���� ��ġ�� ����
        damageText.transform.position = transform.position;

        // ������ �ؽ�Ʈ �ʱ�ȭ
        damageText.Init(atk);
    }

    /// <summary>
    /// ���� ����ٴϵ���
    /// </summary>
    public void FollowHero()
    {
        // ���� �׾����� ����ٴ��� �ʰ� �׳� ����
        if (_isDead == true)
            return;

        Vector2 targetPos = PlaySceneManager.Instance.MyHeroObj.transform.position;

        // ���� ��ġ���� Ÿ����ġ�� �̵�
        transform.position = Vector2.MoveTowards(transform.position, targetPos, _speed * Time.fixedDeltaTime);

        // Ÿ�ٺ��� �� �ڽ�(����)�� �����ʿ� ������
        if (targetPos.x < transform.position.x)
        {
            // ���� �ٶ󺸰�
            _spriteRenderer.flipX = true;
        }
        else
        {
            // �������� ������ �ٶ󺸰�
            _spriteRenderer.flipX = false;
        }
    }
}
