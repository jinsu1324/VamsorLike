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
    private bool _isItemPickUp = false;    // �������� �ֿ����� ����


    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Initialized()
    {
        _isItemPickUp = false;
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
        _isItemPickUp = true;
        PlaySceneManager.Instance.ItemManager.RemoveFieldItemList(this);
        Destroy(this.gameObject);
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
