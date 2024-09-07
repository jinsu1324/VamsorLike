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
    public int Hp { get; set; }
    public int Atk { get; set; }
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
    public void HPMinus(int atk)
    {
        Hp -= atk;


        // 스프라이트 깜빡이기
        BlinkSprite blinkSprite = new BlinkSprite();
        StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));

        if (Hp < 0)
            Death();
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


    // 영웅 따라다니도록
    public void FollowHero()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            PlaySceneManager.ThisGameHeroObject.transform.position,
            Speed * Time.fixedDeltaTime);
    }
}
