using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D m_Rigidbody2D;

    private bool isMouseDown;
    private Vector2 LastPos;

    private LevelSM sm;

    public bool isMovable;
    public bool isClicked;

    public Rigidbody2D Rigidbody2D { get { return m_Rigidbody2D; } }
     
    private void Awake()
    {
        isClicked = false;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        sm = GetComponentInParent<LevelSM>();
    }

    void OnMouseDown()
    {
        if (isClicked == false)
            isClicked = true;
    }


    private void Update()
    {
        
    }

    public void MoveTo(Vector2 target)
    {
        m_Rigidbody2D.velocity = (target - m_Rigidbody2D.position) / 0.5f;
        StartCoroutine("MoveAndStop");
    }

    IEnumerator MoveAndStop()
    {
        yield return new WaitForSeconds(0.5f);
        m_Rigidbody2D.velocity = Vector2.zero;
    }

    /*
    private int CheckProgress()
    {
        // 继承时重载
        // 检查当前进度
        // 检查碰撞

        // 改变进度变量
        return 0;
    }
    */

}

