using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public GameObject monster;
    private void Start()
    {
        StartCoroutine(DD());
    }

    private void Update()
    {

    }

    IEnumerator DD()
    {
        int count = 0;

        while (count < 10)
        {
            Instantiate(monster, new Vector3(-1.1f, 2.36f, 0), Quaternion.identity);
            count++;
            yield return new WaitForSeconds(1.0f);
        }

    }
}
