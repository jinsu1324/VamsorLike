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

    protected bool _isDead = false;                   // ���� �׾����� 

    /// <summary>
    /// ������ ����
    /// </summary>
    public abstract void DataSetting();

    /// <summary>
    /// ������ ���
    /// </summary>
    public abstract void DropItem();

    /// <summary>
    /// ���� ó��
    /// </summary>
    public virtual void Death()
    {
        // ������ ���
        DropItem();

        // �ʵ� �� ��Ͽ��� ����
        PlaySceneManager.Instance.EnemyManager.RemoveFieldEnemyList(this);

        // �ٽ� ������Ʈ Ǯ�� ����������
        PlaySceneManager.Instance.EnemySpawner.EnemyBackTrans(this);

        // ��Ż �� ����Ƚ�� ����
        PlaySceneManager.Instance.AchivementManager.AddKillCount();
    }

    /// <summary>
    /// �״� �ִϸ��̼� ���
    /// </summary>
    public void PlayDeathAnim() 
    {
        // �׾����� true ��
        _isDead = true;

        // �ִϸ��̼� ���
        _animator.SetTrigger("isDead");
    }
      
    /// <summary>
    /// ������ ��� ������ ��� ����
    /// </summary>
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Layer.Hero.ToString()))
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
        // ���� �׾����� ����ٴ��� �ʰ� �׳� ����
        if (_isDead == true)
        {
            Debug.Log("����!");
            return;
        }
            

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
