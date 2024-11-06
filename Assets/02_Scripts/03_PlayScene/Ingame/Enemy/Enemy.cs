using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : ObjectPoolObject
{
    protected float _hp;                              // 오브젝트 HP
    protected float _atk;                             // 오브젝트 Atk
    protected float _speed;                           // 오브젝트 Speed

    protected SpriteRenderer _spriteRenderer;         // 스프라이트 렌더러  

    /// <summary>
    /// 데이터 셋팅
    /// </summary>
    public abstract void DataSetting();

    /// <summary>
    /// 아이템 드랍
    /// </summary>
    public abstract void DropItem();

    /// <summary>
    /// 죽음
    /// </summary>
    public virtual void Death()
    {
        // 필드 적 목록에서 제거
        PlaySceneManager.Instance.EnemyManager.RemoveFieldEnemyList(this);

        // 다시 오브젝트 풀로 돌려보내기
        PlaySceneManager.Instance.EnemySpawner.EnemyBackTrans(this);

        // 토탈 적 죽인횟수 증가
        PlaySceneManager.Instance.AchivementManager.AddKillCount();
        
        // 아이템 드랍
        DropItem();
    }
      
    /// <summary>
    /// 공격 (영웅 충돌 감지로 인해)
    /// </summary>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Layer.Hero.ToString()))
        {
            collision.gameObject.GetComponent<HeroObj>().HPMinus(_atk);
        }
    }

    /// <summary>
    /// HP 감소
    /// </summary>
    public virtual void HPMinus(float atk)
    {
        _hp -= atk;

        // 데미지 텍스트 UI 생성하고 띄워주기
        TurnOn_DamageTextUI_fromPool(atk);

        // 스프라이트 깜빡이기
        BlinkSprite blinkSprite = new BlinkSprite();
        StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));        
    }

    /// <summary>
    /// 데미지텍스트UI 생성하고 띄워주는 함수
    /// </summary>
    public void TurnOn_DamageTextUI_fromPool(float atk)
    {
        // 데미지 텍스트 오브젝트 풀에서 가져와 생성하기
        GameObject go = PlaySceneCanvas.Instance.DamageTextsPool.GetObj();
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
        transform.position = Vector2.MoveTowards(
            transform.position, 
            PlaySceneManager.Instance.MyHeroObj.transform.position, 
            _speed * Time.fixedDeltaTime);
    }
}
