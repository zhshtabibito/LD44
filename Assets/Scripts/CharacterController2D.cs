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
    private Vector3 LastMousePos;
    private LevelSM sm;

    public bool isMovable;
    public bool isClicked;

    public Rigidbody2D Rigidbody2D { get { return m_Rigidbody2D; } }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        sm = GetComponentInParent<LevelSM>();
    }

    private void Update()
    {
        /*** mouse drag ***/

        if (isMovable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isMouseDown = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isMouseDown = false;
                LastMousePos = Vector3.zero; //这里要归零，不然会有漂移效果
                if (sm.CheckProgress() > 0)
                {
                    // nothing happens
                }
                else
                {
                    transform.position = LastPos;
                }
            }
            if (isMouseDown)
            {
                if (LastMousePos != Vector3.zero)
                {
                    Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - LastMousePos;
                    transform.position += offset;
                }
                LastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
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

