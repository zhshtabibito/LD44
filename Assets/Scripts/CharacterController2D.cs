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
    private bool isArrival;

    public int id;
    public bool isMovable;
    private bool isClicked;
    public GameObject Target = null;

    public Rigidbody2D Rigidbody2D { get { return m_Rigidbody2D; } }
     
    private void Awake()
    {
        //TODO
        this.gameObject.transform.parent = GameObject.Find("World").transform;
        isClicked = false;
        isArrival = false;
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
        Vector3 StartPos = new Vector3(trans.position.x, trans.position.y, trans.position.z);
        Vector3 LastPos = StartPos;
        if(isMovable)
　　        while(Input.GetMouseButton(0))
            {
                if(isArrival)
                {
                    isMovable = false;
                    sm.ObjectClicked(id, this.gameObject);
                    break;
                }
　　　　        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, trans.position.z);
                Vector3 targetPos = offset + Camera.main.ScreenToWorldPoint(mousePos);
　　　　        trans.position = targetPos;
                LastPos = targetPos;
　　　　        yield return new WaitForFixedUpdate();
            }
        Debug.Log(LastPos);
        float distance = (LastPos - StartPos).sqrMagnitude;

        if(distance < 0.05 && !isClicked)
        {
            isClicked = true;
            sm.ObjectClicked(id, this.gameObject);
        }
        if (!isArrival)
            this.gameObject.transform.position = StartPos;
　　}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.gameObject == Target)
        {
            this.transform.position = other.transform.position;
            isArrival = true;
        }
    }

    public void MoveSpd(Vector2 spd)
    {
        m_Rigidbody2D.velocity = spd;
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

    public void MoveToSlow(Vector2 target)
    {
        m_Rigidbody2D.velocity = (target - m_Rigidbody2D.position) / 2f;
        StartCoroutine("MoveAndStopSlow");
    }

    IEnumerator MoveAndStopSlow()
    {
        yield return new WaitForSeconds(2);
        m_Rigidbody2D.velocity = Vector2.zero;
    }

}

