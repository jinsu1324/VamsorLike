using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolObject : SerializedMonoBehaviour
{
    // ���̾��Ű���� ������ �θ�
    private Transform _parent;

    // �θ� ����
    public void Setting(Transform trans)
    {
        _parent = trans;
    }

    
    // ���� (����ϱ� ���� ON)
    public virtual void Spawn()
    {
        gameObject.SetActive(true);
    }

    // �����ϰ� �Ǿ����� �ٽ� ����������
    public virtual void BackTrans()
    {
        gameObject.SetActive(false);

        if (_parent != null)
            transform.SetParent(_parent);

        transform.SetAsFirstSibling();
    }    
}
