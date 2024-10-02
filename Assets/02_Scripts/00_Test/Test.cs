using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Rigidbody2D _rigid;

    // 이동에 사용할 vector2 dir
    private Vector2 _moveDir;

    public Joystick _joystick;

    private float Speed = 3.0f;


    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");

        float horizontal = _joystick.Horizontal;
        float vertical = _joystick.Vertical;

        _moveDir.x = _rigid.position.x + (horizontal * Speed * Time.deltaTime);
        _moveDir.y = _rigid.position.y + (vertical * Speed * Time.deltaTime);

        _rigid.MovePosition(_moveDir);
    }
}


