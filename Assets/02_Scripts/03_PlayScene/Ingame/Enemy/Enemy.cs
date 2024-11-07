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

    protected Animator _animator;                     // 애니메이터
    protected SpriteRenderer _spriteRenderer;         // 스프라이트 렌더러  

    protected bool _isDead = false;                   // 적이 죽었는지 

    /// <summary>
    /// 데이터 셋팅
    /// </summary>
    public abstract void DataSetting();

    /// <summary>
    /// 아이템 드랍
    /// </summary>
    public abstract void DropItem();

    /// <summary>
    /// 죽음 처리
    /// </summary>
    public virtual void Death()
    {
        // 아이템 드랍
        DropItem();

        // 필드 적 목록에서 제거
        PlaySceneManager.Instance.EnemyManager.RemoveFieldEnemyList(this);

        // 다시 오브젝트 풀로 돌려보내기
        PlaySceneManager.Instance.EnemySpawner.EnemyBackTrans(this);

        // 토탈 적 죽인횟수 증가
        PlaySceneManager.Instance.AchivementManager.AddKillCount();
    }

    /// <summary>
    /// 죽는 애니메이션 재생
    /// </summary>
    public void PlayDeathAnim() 
    {
        // 죽었음을 true 로
        _isDead = true;

        // 애니메이션 재생
        _animator.SetTrigger("isDead");
    }
      
    /// <summary>
    /// 영웅에 닿고 있으면 계속 공격
    /// </summary>
    public void OnCollisionStay2D(Collision2D collision)
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
        // 적이 죽었으면 따라다니지 않고 그냥 리턴
        if (_isDead == true)
        {
            Debug.Log("죽음!");
            return;
        }
            

        Vector2 targetPos = PlaySceneManager.Instance.MyHeroObj.transform.position;

        // 현재 위치에서 타겟위치로 이동
        transform.position = Vector2.MoveTowards(transform.position, targetPos, _speed * Time.fixedDeltaTime);

        // 타겟보다 나 자신(몬스터)가 오른쪽에 있으면
        if (targetPos.x < transform.position.x)
        {
            // 왼쪽 바라보게
            _spriteRenderer.flipX = true;
        }
        else
        {
            // 나머지는 오른쪽 바라보게
            _spriteRenderer.flipX = false;
        }
    }
}
