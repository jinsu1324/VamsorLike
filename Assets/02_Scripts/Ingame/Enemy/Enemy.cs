using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : ObjectPoolObject
{
    public static event Action<Enemy> OnEnemyDead;   // 적 죽었을때 처리될 delegate

    protected float _hp;                              // 오브젝트 HP
    protected float _atk;                             // 오브젝트 Atk
    protected float _speed;                           // 오브젝트 Speed

    protected SpriteRenderer _spriteRenderer;         // 스프라이트 렌더러  
    protected NavMeshAgent _navMeshAgent;             // 네비메쉬 에이전트


    /// <summary>
    /// 데이터 셋팅
    /// </summary>
    public abstract void DataSetting();

    /// <summary>
    /// 죽음
    /// </summary>
    public virtual void Death()
    {
        OnEnemyDead?.Invoke(this);
    }    

    /// <summary>
    /// 공격 (영웅 충돌 감지로 인해)
    /// </summary>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {
            collision.gameObject.GetComponent<HeroObj>().HPMinus(_atk);
        }
    }

    /// <summary>
    /// HP 감소
    /// </summary>
    public void HPMinus(float atk)
    {
        _hp -= atk;

        // 데미지 텍스트 UI 생성하고 띄워주기
        TurnOn_DamageTextUI_fromPool(atk);

        // 스프라이트 깜빡이기
        BlinkSprite blinkSprite = new BlinkSprite();
        StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));

        // HP 0 이하면 죽음
        if (_hp < 0)
            Death();
    }

    /// <summary>
    /// 데미지텍스트UI 생성하고 띄워주는 함수
    /// </summary>
    public void TurnOn_DamageTextUI_fromPool(float atk)
    {
        // 데미지 텍스트 오브젝트 풀에서 가져와 생성하기
        GameObject go = PlaySceneManager.Instance.PlaySceneCanvas.DamageTextsPool.GetObj();
        DamageText damageTextUI = go.GetComponent<DamageText>();

        // 생성 포지션 설정
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        RectTransform rectTransform = damageTextUI.GetComponent<RectTransform>();
        rectTransform.position = screenPosition;

        // 데미지 텍스트 초기화
        damageTextUI.Init(atk);
    }

    /// <summary>
    /// 영웅 따라다니도록
    /// </summary>
    public void FollowHero()
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(PlaySceneManager.Instance.MyHeroObj.transform.position);
    }

    /// <summary>
    /// 따라다니는것 멈추기
    /// </summary>
    public void Stop()
    {
        _navMeshAgent.isStopped = true;
    }
}
