using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemID
{
    EXP,
    Gold,
    RewardBox,
    Heal,
    Magnet
}

public abstract class ItemBase : ObjectPoolObject
{
    private bool _isItemPickUp = false;              // 아이템을 주웠는지 여부

    [SerializeField]
    private Animator _animator;                      // 애니메이터

    protected string _pickUpAnimClipName = "PickUp";   // 픽업 애니메이션 이름 string
    protected float _pickUpAnimClipLength;             // 픽업 클립 길이 담을 변수

    /// <summary>
    /// 스폰 시 초기화
    /// </summary>
    public void Initialize()
    {
        _animator.SetBool("isPickUp", false);
        _isItemPickUp = false;

        _pickUpAnimClipLength = AnimSupporter.GetAnimationClipLength(_pickUpAnimClipName, _animator);
        
        PlaySceneManager.Instance.ItemManager.AddFieldItemList(this);
    }

    /// <summary>
    /// 아이템이 영웅에 닿으면 실행
    /// </summary>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Layer.Hero.ToString()))
        {
            ItemPickUp(collision);
        }
    }

    /// <summary>
    /// 아이템을 주웠을 때 행동을 정의할 추상 함수
    /// </summary>
    protected virtual void ItemPickUp(Collider2D collision)
    {
        _animator.SetBool("isPickUp", true);

        _isItemPickUp = true;
        
        PlaySceneManager.Instance.ItemManager.RemoveFieldItemList(this);

        BackTrans_AfterTime(_pickUpAnimClipLength);
    }
    
    /// <summary>
    /// 아이템 이동 코루틴 시작
    /// </summary>
    public void StartMoveItemToHero(float speed)
    {
        StartCoroutine(MoveItemToHero(speed));
    }

    /// <summary>
    /// 아이템을 영웅 위치로 이동
    /// </summary>
    private IEnumerator MoveItemToHero(float speed)
    {
        // 아이템이 플레이어에게 가까이 갈 때까지 반복
        while (Vector3.Distance(transform.position, PlaySceneManager.Instance.MyHeroObj.transform.position) > 0.1f && _isItemPickUp == false)
        {
            // 아이템을 플레이어 쪽으로 이동시킴
            transform.position = Vector3.MoveTowards(
                transform.position,
                PlaySceneManager.Instance.MyHeroObj.transform.position,
                speed * Time.deltaTime);

            yield return null;
        }
    }
}
