using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    ObjectPoolObj prefab = null;

    [SerializeField]
    int pluscount = 10;

    private void Awake()
    {
        CreateObjs();
    }

    void CreateObjs()
    {
        for (int i = 0; i < pluscount; ++i)
        {
            ObjectPoolObj obj = Instantiate(prefab, transform);

            obj.gameObject.SetActive(false);

            obj.Setting(transform);
        }
    }

    public GameObject GetObj()
    {
        if (transform.childCount <= 0)
        {
            CreateObjs();
        }

        int count = 0;

        GameObject returnobj = transform.GetChild(count).gameObject;

        while (returnobj.activeInHierarchy)
        {
            //���⼭ ��� �����³༮ �״�� �������� �װ� ����������մϴ�~
            if (++count >= transform.childCount)
                CreateObjs();

            returnobj = transform.GetChild(count).gameObject;
        }

        returnobj.transform.localScale = Vector3.one;

        return returnobj;
    }

    public GameObject GetObj(Transform _trans)
    {
        if (transform.childCount <= 0)
        {
            CreateObjs();
        }

        GameObject returnobj = transform.GetChild(0).gameObject;

        returnobj.transform.SetParent(_trans);

        returnobj.transform.localScale = Vector3.one;
        return returnobj;
    }
}

public class ObjectPool<T> : MonoBehaviour where T : ObjectPoolObj
{
    [SerializeField]
    T prefab = null;

    [SerializeField]
    int pluscount = 10;

    List<T> Objs = new List<T>();

    private void Awake()
    {
        CreateObjs();
    }

    void CreateObjs()
    {
        for (int i = 0; i < pluscount; ++i)
        {
            T obj = Instantiate(prefab, transform);

            obj.gameObject.SetActive(false);

            obj.Setting(transform, delegate
            {
                //Debug.Log("Add");
                Objs.Add(obj);
            });

            //Debug.Log("Add");
            Objs.Add(obj);
        }

        //Debug.Log("Plus");
    }

    public T GetObj()
    {
        if (Objs.Count <= 0)
        {
            CreateObjs();
        }

        int count = 0;

        T returnobj = Objs[count];

        while (returnobj.gameObject.activeInHierarchy)
        {
            if (++count >= Objs.Count)
                CreateObjs();

            returnobj = Objs[count];
        }

        returnobj.transform.localScale = Vector3.one;

        //Debug.Log("Remove");

        Objs.Remove(returnobj);

        return returnobj;
    }

    public T GetObj(Transform _trans)
    {
        if (Objs.Count <= 0)
        {
            CreateObjs();
        }

        T returnobj = Objs[0];

        Objs.Remove(returnobj);

        return returnobj;
    }
}
