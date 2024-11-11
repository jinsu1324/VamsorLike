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
    private bool _isItemPickUp = false;              // �������� �ֿ����� ����

    [SerializeField]
    private Animator _animator;                      // �ִϸ�����

    protected string _pickUpAnimClipName = "PickUp";   // �Ⱦ� �ִϸ��̼� �̸� string
    protected float _pickUpAnimClipLength;             // �Ⱦ� Ŭ�� ���� ���� ����

    /// <summary>
    /// ���� �� �ʱ�ȭ
    /// </summary>
    public void Initialize()
    {
        _animator.SetBool("isPickUp", false);
        _isItemPickUp = false;

        _pickUpAnimClipLength = AnimSupporter.GetAnimationClipLength(_pickUpAnimClipName, _animator);
        
        PlaySceneManager.Instance.ItemManager.AddFieldItemList(this);
    }

    /// <summary>
    /// �������� ������ ������ ����
    /// </summary>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Layer.Hero.ToString()))
        {
            ItemPickUp(collision);
        }
    }

    /// <summary>
    /// �������� �ֿ��� �� �ൿ�� ������ �߻� �Լ�
    /// </summary>
    protected virtual void ItemPickUp(Collider2D collision)
    {
        _animator.SetBool("isPickUp", true);

        _isItemPickUp = true;
        
        PlaySceneManager.Instance.ItemManager.RemoveFieldItemList(this);

        BackTrans_AfterTime(_pickUpAnimClipLength);
    }
    
    /// <summary>
    /// ������ �̵� �ڷ�ƾ ����
    /// </summary>
    public void StartMoveItemToHero(float speed)
    {
        StartCoroutine(MoveItemToHero(speed));
    }

    /// <summary>
    /// �������� ���� ��ġ�� �̵�
    /// </summary>
    private IEnumerator MoveItemToHero(float speed)
    {
        // �������� �÷��̾�� ������ �� ������ �ݺ�
        while (Vector3.Distance(transform.position, PlaySceneManager.Instance.MyHeroObj.transform.position) > 0.1f && _isItemPickUp == false)
        {
            // �������� �÷��̾� ������ �̵���Ŵ
            transform.position = Vector3.MoveTowards(
                transform.position,
                PlaySceneManager.Instance.MyHeroObj.transform.position,
                speed * Time.deltaTime);

            yield return null;
        }
    }
}
