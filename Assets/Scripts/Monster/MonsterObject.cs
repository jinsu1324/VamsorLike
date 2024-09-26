using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

// 몬스터 게임오브젝트 : 몬스터관련 데이터 / 본인 데이터 이닛 / 공격 / 플레이어 따라 이동 / HP감소 / 죽음
public class MonsterObject : ObjectPoolObject
{
    [Title("데이터 본체", bold: false)]
    // 몬스터 오브젝트에 들어갈 데이터
    [SerializeField]
    private readonly MonsterData _baseMonsterData;

    // 오브젝트에 할당될 데이터
    public float Hp { get; set; }
    public float Atk { get; set; }
    public float Speed { get; set; }

    // 스프라이트 렌더러
    private SpriteRenderer _spriteRenderer;

    // 몬스터 죽었을때 처리될 함수들 액션
    public static event Action<MonsterObject> OnMonsterDeath;


    // 데이터 셋팅
    public void DataSetting()
    {
        // 데이터 할당
        Hp = _baseMonsterData.MaxHp;
        Atk = _baseMonsterData.Atk;
        Speed = _baseMonsterData.Speed;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        MonsterManager.Instance.AddFieldMonsterList(this);
    }

    // HP 감소
    public void HPMinus(float atk)
    {
        Hp -= atk;

        // 데미지 텍스트 UI 생성하고 띄워주기
        TurnOn_DamageTextUI_fromPool(atk);

        // 스프라이트 깜빡이기
        BlinkSprite blinkSprite = new BlinkSprite();
        StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));

        if (Hp < 0)
            Death();
    }


    // 영웅 따라다니도록
    public void FollowHero()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            PlaySceneManager.ThisGameHeroObject.transform.position,
            Speed * Time.fixedDeltaTime);
    }



    // 죽음
    private void Death()
    {
        // 몬스터 죽었을때 액션들 실행 (필드 몬스터 리스트에서 이 몬스터 삭제 / 바닥에 경험치 떨구기 / 다시 오브젝트 풀로 돌려보내기)
        OnMonsterDeath?.Invoke(this);

        Destroy(this.gameObject);
    }


    // 영웅 충돌감지 및 공격
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {
            collision.gameObject.GetComponent<HeroObject>().HPMinus(Atk);
        }
    }


    /// <summary>
    /// 데미지텍스트UI 생성하고 띄워주는 함수
    /// </summary>
    private void TurnOn_DamageTextUI_fromPool(float atk)
    {
        // 데미지 텍스트 오브젝트 풀에서 가져와 생성하기
        GameObject go = PlaySceneManager.Instance.DamageTextUIPool.GetObj();
        DamageTextUI damageTextUI = go.GetComponent<DamageTextUI>();

        // 생성 포지션 설정
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        RectTransform rectTransform = damageTextUI.GetComponent<RectTransform>();
        rectTransform.position = screenPosition;

        // 데미지 텍스트 초기화
        damageTextUI.Init(atk);
    }
}
