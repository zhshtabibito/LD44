using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D m_Rigidbody2D;
    CapsuleCollider2D m_Capsule;

    Vector2 m_PreviousPosition;
    Vector2 m_CurrentPosition;
    Vector2 m_NextMovement;
    // Start is called before the first frame update

    public Vector2 Velocity { get; protected set; }
    public Rigidbody2D Rigidbody2D { get { return m_Rigidbody2D; } }
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        m_PreviousPosition = m_Rigidbody2D.position;
        m_CurrentPosition = m_PreviousPosition + m_NextMovement;
        m_Rigidbody2D.MovePosition(m_CurrentPosition);
        m_NextMovement = Vector2.zero;
    }

    public void Move(Vector2 movement)
    {
        m_NextMovement += movement;
    }
}

