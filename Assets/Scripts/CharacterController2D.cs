using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D m_Rigidbody2D;

    private Vector2 LastMousePos;

    private LevelSM sm;
    private bool isArrival;

    public int id;
    public int idReserved;
    public int idReserved2;
    public bool isMovable;
    public bool willBack;
    public bool willStick;
    private bool isClicked;
    public GameObject Target = null;
    public GameObject TargetReserved = null;
    public GameObject TargetReserved2 = null;

    Vector3 StartPos;

    public Rigidbody2D Rigidbody2D { get { return m_Rigidbody2D; } }

    private void Awake()
    {
        StartPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

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
        Vector3 LastPos = StartPos;
        if(isMovable)
　　        while(Input.GetMouseButton(0))
            {
　　　　        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, trans.position.z);
                Vector3 targetPos = offset + Camera.main.ScreenToWorldPoint(mousePos);
　　　　        trans.position = targetPos;
                LastPos = targetPos;
　　　　        yield return new WaitForFixedUpdate();
            }
        Debug.Log(LastPos);
        float distance = (LastPos - StartPos).sqrMagnitude;

        if(distance < 0.05 && !isClicked && Target == null)
        {
            isClicked = true;
            sm.ObjectClicked(id, this.gameObject);
        }
　　}

    IEnumerator OnMouseUp()
    {
        if (isMovable)
        {
            if (!isArrival)
                this.gameObject.transform.position = StartPos;
            else
            {
                isMovable = false;
                sm.ObjectClicked(id, this.gameObject);
                transform.position = willBack ? StartPos : Target.transform.position;
            }
        }
        yield return new WaitForFixedUpdate();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.gameObject == Target)
        {
            if (willStick)
            {
                isMovable = false;
                sm.ObjectClicked(id, this.gameObject);
            }
            isArrival = true;
        }
        else if (other.transform.gameObject == TargetReserved)
        {
            isArrival = true;
            Target = TargetReserved;
            id = idReserved;
        }
        else if (other.transform.gameObject == TargetReserved2)
        {
            isArrival = true;
            Target = TargetReserved2;
            id = idReserved2;
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

