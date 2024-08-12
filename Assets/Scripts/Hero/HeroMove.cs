using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    // 내 오브젝트의 HeroObject
    private HeroObject _heroObject;

    private Rigidbody2D _rigid;
    private Vector2 _moveDir;

    private void Start()
    {
        _heroObject = GetComponent<HeroObject>();

        _rigid = GetComponent<Rigidbody2D>();
        _moveDir = Vector2.zero;
    }

    private void FixedUpdate()
    {
        Move();
    }    
    
    // 이동
    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _moveDir.x = _rigid.position.x + (horizontal * _heroObject.Speed * Time.deltaTime);
        _moveDir.y = _rigid.position.y + (vertical * _heroObject.Speed * Time.deltaTime);

        _rigid.MovePosition(_moveDir);
    }








    //private void Update()
    //{
    //    Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, _range, LayerMask.GetMask("Enemy"));


    //    for (int i = 0; i < cols.Length; i++)
    //    {
    //        Debug.Log(cols[i].gameObject.name);
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, _range);
    //}
}
