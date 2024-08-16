using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

// 몬스터 게임오브젝트 : 몬스터관련 데이터 / 본인 데이터 이닛 / 공격 / 플레이어 따라 이동 / HP감소 / 죽음
public class MonsterObject : SerializedMonoBehaviour
{
    [Title("데이터 본체", bold: false)]
    // 몬스터 오브젝트에 들어갈 데이터
    [SerializeField]
    private readonly MonsterData _monsterData;

    [Title("할당될 데이터 값들", bold: false)]
    // 이름
    [SerializeField]
    public string Name { get; set; }

    // 체력
    [SerializeField]
    public int Hp { get; set; }

    // 공격력
    [SerializeField]
    public int Atk { get; set; }

    // 이동속도
    [SerializeField]
    public float Speed { get; set; }

    // 스프라이트 렌더러
    private SpriteRenderer _spriteRenderer;

    private void FixedUpdate()
    {
        // 영웅 따라다니도록
        FollowHero();
    }

    // 데이터 셋팅
    public void DataSetting()
    {
        Name = _monsterData.Name;
        Hp = _monsterData.Hp;
        Atk = _monsterData.Atk;
        Speed = _monsterData.Speed;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        SceneSingleton<MonsterManager>.Instance.SpawnMonster(this);
    }

    // HP 감소
    public void HPMinus(int atk)
    {
        Hp -= atk;

        // 스프라이트 깜빡이기
        //BlinkSprite blinkSprite = new BlinkSprite();
        //StartCoroutine(blinkSprite.Blink(_spriteRenderer, 0.1f));

        if (Hp < 0)
            Death();
    }


    // 죽음
    private void Death()
    {
        SceneSingleton<MonsterManager>.Instance.DieMonster(this);
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
    private void FollowHero()
    {
        this.gameObject.transform.position = 
            Vector3.Lerp(this.gameObject.transform.position, 
            PlaySceneManager.ThisGameHeroObject.transform.position,
            Speed * Time.deltaTime);
    }
}
