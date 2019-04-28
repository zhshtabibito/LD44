﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCar3 : LevelSM
{
    private LevelManager lm;

    public GameObject car;
    public GameObject grandma;
    public GameObject father;
    public GameObject driver1;
    public GameObject driver2;
    public GameObject driver3;
    public GameObject bird;
    public GameObject egg;
    public GameObject lightRG;


    public bool canClear1;
    public bool canClear2;

    // Start is called before the first frame update
    void Start()
    {
        canClear1 = false;
        canClear2 = false;
        lm = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ObjectClicked(int id, GameObject obj)
    {
        if (id == 1 && canClear1 == false) // Grandma
        {
            Debug.Log("G");
            canClear1 = true;
            StartCoroutine("WaitAndMove");
        }
        else if (id == 2 && canClear2 == false) // bird
        {
            Debug.Log("W");
            canClear2 = true;
            StartCoroutine("WaitAndMove");
        }
    }

    IEnumerator WaitAndMove()
    {
        if (canClear1 && canClear2)
        {
            yield return new WaitForSeconds(2);
            grandma.GetComponent<CharacterController2D>().MoveSpd(new Vector2(-0.5f, -0.5f));
            father.GetComponent<CharacterController2D>().MoveSpd(new Vector2(-4, 0));
            car.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, -1));
            father.GetComponent<Animator>().SetTrigger("Idle2Run");
            StartCoroutine("WaitAndStop");
        }

    }

    IEnumerator WaitAndStop()
    {

        yield return new WaitForSeconds(0.7f);
        father.GetComponent<CharacterController2D>().MoveSpd(Vector2.zero);
        car.GetComponent<CharacterController2D>().MoveSpd(Vector2.zero);
        Debug.Log("Die");
        father.GetComponent<Animator>().SetTrigger("Run2Die");
        // clear and UI
        lm.LevelClear();
    }
}
