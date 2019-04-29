using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCthulu1 : LevelCthuluTemplate
{

    public GameObject PaganFather;
    public GameObject Bubble;
    private LevelManager lm;

    // Start is called before the first frame update
    void Start()
    {
        lm = FindObjectOfType<LevelManager>();
        state = 0;
        BlueBook.SetActive(false);
        YellowBook.SetActive(false);
        DeadBook.SetActive(false);
        Magic.SetActive(false);
        DarkBackground2.SetActive(false);
    }


    public override void ObjectClicked(int id, GameObject obj)
    {
        if (id == 1)
        {
            obj.SetActive(false);
            state = 2;
        }
        else if (id == 2)
        {
            if (state < 5)
            {
                obj.GetComponent<CharacterController2D>().isClicked = false;
                if (state == 2)
                {
                    state = 3;
                    BlueBook.SetActive(true);
                    Dropto(BlueBook, -3.2f);
                }
                else if (state == 3)
                {
                    state = 4;
                    YellowBook.SetActive(true);
                    Dropto(YellowBook, -3.2f);
                }
                else if (state == 4)
                {
                    state = 5;
                    DeadBook.SetActive(true);
                    Dropto(DeadBook, -3.2f);
                    obj.GetComponent<CharacterController2D>().isClicked = true;
                }
            }
        }
        else if(id == 3)
        {
            if (state == 5)
            {
                StartCoroutine("WaitForMove");
            }
        }
    }

    IEnumerator WaitForMove()
    {
        DeadBook.SetActive(false);
        DarkBackground2.SetActive(true);
        Magic.SetActive(true);
        ObjectReplace(Father, PaganFather);
        SetMovement(Cthulu, PaganFather, new Vector2(-3.2f, 12.0f), new Vector2(-3.2f, 1.5f), 3.0f);
        yield return new WaitForSeconds(4);
        state = CLEAR;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == CLEAR)
        {
            //StartCoroutine("WaitForClear", lm);
            lm.LevelClear();
            state = CLEAR - 1;
        }
    }
}
