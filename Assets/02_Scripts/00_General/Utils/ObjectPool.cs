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
        CreateObjs();
    }


    // ������Ʈ ����
    private void CreateObjs()
    {
        for (int i = 0; i < _count; i++)
        {
            ObjectPoolObject obj = Instantiate(_prefab, transform);

            obj.gameObject.SetActive(false);

            obj.Setting(transform);
        }
    }

    // ������Ʈ ����Ҷ� ��������
    public GameObject GetObj()
    {
        if (transform.childCount <= 0)
            CreateObjs();

        int count = 0;

        GameObject returnObj = transform.GetChild(count).gameObject;

        while (returnObj.activeInHierarchy) // -----> ù��° �ڽ��� �����־ true��, count�� �÷��༭ �ٽ� �����ڽ��� �޾ƿͺ�
        {
            if (++count >= transform.childCount)
                CreateObjs();

            returnObj = transform.GetChild(count).gameObject;
        }

        //returnObj.transform.localScale = Vector3.one;

        returnObj.GetComponent<ObjectPoolObject>().Spawn();

        return returnObj;
    }
}
