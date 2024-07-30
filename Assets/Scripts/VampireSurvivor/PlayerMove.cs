using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _range;

    private Rigidbody2D _rigid;
    private Vector2 _moveDir;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _moveDir = Vector2.zero;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    private void Update()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, _range, LayerMask.GetMask("Enemy"));


        for (int i = 0; i < cols.Length; i++)
        {
            Debug.Log(cols[i].gameObject.name);
        }
        

    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _moveDir.x = _rigid.position.x + (horizontal * _speed * Time.deltaTime);
        _moveDir.y = _rigid.position.y + (vertical * _speed * Time.deltaTime);

        _rigid.MovePosition(_moveDir);
    }
}
