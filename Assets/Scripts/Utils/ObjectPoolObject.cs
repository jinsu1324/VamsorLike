using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class ObjectPoolObj : SerializedMonoBehaviour
{
    Transform _parent;
    Action backact;

    public void Setting(Transform _trans)
    {
        _parent = _trans;
    }

    public void Setting(Transform _trans, Action _act)
    {
        _parent = _trans;
        backact = _act;
    }

    public virtual void BackTrans()
    {
        gameObject.SetActive(false);

        if (_parent != null)
            transform.SetParent(_parent);

        backact?.Invoke();
    }
}

public class ObjectPoolObj<T> : SerializedMonoBehaviour
{
    System.Action<ObjectPoolObj<T>> BackTransAct;

    T Compontent;

    Transform _parent;

    public void Setting(Transform _trans, System.Action<ObjectPoolObj<T>> _act)
    {
        BackTransAct = _act;
        Compontent = gameObject.GetComponent<T>();

        _parent = _trans;
    }

    public T GetComponent()
    {
        if (Compontent == null)
            Compontent = gameObject.GetComponent<T>();

        return Compontent;
    }

    public virtual void BackTrans()
    {
        gameObject.SetActive(false);

        transform.SetParent(_parent);

        BackTransAct?.Invoke(this);
    }
}