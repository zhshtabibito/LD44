﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D m_Rigidbody2D;

    private Vector2 LastMousePos;

    private LevelSM sm;

    public int id;
    public bool isMovable;
    public bool isClicked;

    public Rigidbody2D Rigidbody2D { get { return m_Rigidbody2D; } }
     
    private void Awake()
    {
        //TODO
        this.gameObject.transform.parent = GameObject.Find("World").transform;
        isClicked = false;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        sm = GetComponentInParent<LevelSM>();
    }

    private void Update()
    {

    }

    IEnumerator OnMouseDown()
    {
        Transform trans = this.gameObject.transform;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, trans.position.z);
        Vector3 offset = trans.position - Camera.main.ScreenToWorldPoint(mousePos);
　　    while(Input.GetMouseButton(0))
        {
　　　　    mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, trans.position.z);
            Vector3 targetPos = offset+Camera.main.ScreenToWorldPoint(mousePos);
　　　　    trans.position = targetPos;
　　　　    yield return new WaitForFixedUpdate();
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

}

