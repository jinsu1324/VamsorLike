using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // 오브젝트 풀에 생성할 프리팹
    [SerializeField]
    private ObjectPoolObject _prefab;

    // 한번에 생성할 갯수
    [SerializeField]
    private int _count = 10;


    private void Awake()
    {
        CreateObj();
    }


    // 오브젝트 생성
    private void CreateObj()
    {
        for (int i = 0; i < _count; i++)
        {
            ObjectPoolObject obj = Instantiate(_prefab, transform);

            obj.gameObject.SetActive(false);

            obj.Setting(transform);
        }
    }

    // 오브젝트 사용헐때 가져오기
    public GameObject GetObj()
    {
        if (transform.childCount <= 0)
            CreateObj();

        int count = 0;

        GameObject childObj = transform.GetChild(count).gameObject;
        
        // 순회하면서 가져온 자식오브젝트가 켜져있다면 (사용중이라면) 다음 자식오브젝트로 넘어감
        if (childObj.activeInHierarchy)
        {
            if (++count >= transform.childCount)
                CreateObj();

            childObj = transform.GetChild(count).gameObject;
        }

        childObj.transform.localScale = Vector3.one;

        childObj.GetComponent<ObjectPoolObject>().Spawn();

        Debug.Log("GetObj");

        return childObj;



    }
}
