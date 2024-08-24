using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // ������Ʈ Ǯ�� ������ ������
    [SerializeField]
    private ObjectPoolObject _prefab;

    // �ѹ��� ������ ����
    [SerializeField]
    private int _count = 10;


    private void Awake()
    {
        CreateObj();
    }


    // ������Ʈ ����
    private void CreateObj()
    {
        for (int i = 0; i < _count; i++)
        {
            ObjectPoolObject obj = Instantiate(_prefab, transform);

            obj.gameObject.SetActive(false);

            obj.Setting(transform);
        }
    }

    // ������Ʈ ����涧 ��������
    public GameObject GetObj()
    {
        if (transform.childCount <= 0)
            CreateObj();

        int count = 0;

        GameObject childObj = transform.GetChild(count).gameObject;
        
        // ��ȸ�ϸ鼭 ������ �ڽĿ�����Ʈ�� �����ִٸ� (������̶��) ���� �ڽĿ�����Ʈ�� �Ѿ
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
