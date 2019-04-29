﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCar2 : LevelSM
{
    private LevelManager lm;

    public GameObject car1;
    public GameObject car2;
    public GameObject grandma;
    public GameObject father;
    public GameObject driver1;
    public GameObject driver2;
    public GameObject driver3;
    public GameObject lightRG;

    // Start is called before the first frame update
    void Start()
    {
        lm = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ObjectClicked(int id, GameObject obj)
    {
        if (id == 1) // Grandma
        {
            StartCoroutine("WaitAndGreen");
        }

    }

    IEnumerator WaitAndGreen()
    {
        yield return new WaitForSeconds(1);
        lightRG.GetComponent<SpriteRenderer>().sprite = GameObject.Find("LightG").GetComponent<SpriteRenderer>().sprite;
        StartCoroutine("WaitAndMove");
    }

    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(2);
        grandma.GetComponent<CharacterController2D>().MoveSpd(new Vector2(-0.5f, -0.5f));
        father.GetComponent<CharacterController2D>().MoveSpd(new Vector2(-4, 0));
        car1.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, -1));
        father.GetComponent<Animator>().SetTrigger("Idle2Run");
        StartCoroutine("WaitAndStop");
    }

    IEnumerator WaitAndStop()
    {
        Debug.Log("WaitAndStop");
        yield return new WaitForSeconds(0.5f);
        car1.GetComponent<CharacterController2D>().MoveSpd(Vector2.zero);
        driver1.SetActive(false);
        driver3.SetActive(true);
        StartCoroutine("WaitAndFail");
    }

    IEnumerator WaitAndFail()
    {
        yield return new WaitForSeconds(0.5f);
        father.GetComponent<CharacterController2D>().MoveSpd(Vector2.zero);
        father.GetComponent<Animator>().SetTrigger("Run2Idle");
        // fail and UI
        yield return new WaitForSeconds(1);
        lm.LevelFail();
        StartCoroutine("WaitAndClear");

    }

    IEnumerator WaitAndClear()
    {
        // TODO: 汽车鸣笛音效
        yield return new WaitForSeconds(5);
        car2.SetActive(true);
        car2.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, -2));
        // TODO: 撞车音效
        car1.GetComponent<CharacterController2D>().MoveSpd(new Vector2(-6, -8));
        car2.GetComponent<CharacterController2D>().MoveSpd(new Vector2(8, -6));
        father.GetComponent<Animator>().SetTrigger("Idle2Die");
        yield return new WaitForSeconds(1);
        var canvasManager = FindObjectsOfType<CanvasManager>()[0];
        canvasManager.CloseFailUI();
        lm.LevelClear();

    }




}
