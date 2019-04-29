﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCafe3 : LevelSM
{
    private LevelManager lm;
    private AudioSource m_Audio;

    public GameObject father;
    public GameObject soul;
    public GameObject chicken;
    public GameObject soup;

    private GameObject KK;
    private GameObject CC;
    private GameObject NN;

    public Sprite Chicken2;
    public Sprite fatherSoup;
    public Sprite fatherDage;

    private bool hasChicken;
    public Sprite SoupChicken;
    public Sprite SoupHalf;
    public Sprite SoupPurple;
    public Sprite SoupChickenPurple;
    public Sprite SoupEmpty;

    private bool K;
    private bool C;
    private bool N;

    public bool canClear;

    private Vector3 posSoup;

    // Start is called before the first frame update
    void Start()
    {
        canClear = false;
        posSoup = soup.transform.position;
        lm = FindObjectOfType<LevelManager>();
        m_Audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ObjectClicked(int id, GameObject obj)
    {
        Debug.Log(id);
        if (id == 1) // K
        {
            K = true;
            KK = obj;
            obj.GetComponent<CharacterController2D>().Target.GetComponent<BoxCollider2D>().enabled = false;
            obj.transform.parent = soup.transform;
            StartCoroutine("SoupChange");
        }
        else if (id == 2) // C
        {
            C = true;
            CC = obj;
            obj.GetComponent<CharacterController2D>().Target.GetComponent<BoxCollider2D>().enabled = false;
            obj.transform.parent = soup.transform;
            StartCoroutine("SoupChange");
        }
        else if(id == 3) // N
        {
            N = true;
            NN = obj;
            obj.GetComponent<CharacterController2D>().Target.GetComponent<BoxCollider2D>().enabled = false;
            obj.transform.parent = soup.transform;
            StartCoroutine("SoupChange");
        }
        else if(id == 4) // soup
        {
            if (canClear)
            {
                StartCoroutine("WaitAndDie");
            }
            else
            {
                StartCoroutine("WaitAndFail");
            }

        }
        else if (id == 5) // chicken pot
        {
            hasChicken = true;
            chicken.GetComponent<SpriteRenderer>().sprite = Chicken2;
            soup.GetComponent<SpriteRenderer>().sprite = SoupChicken;

        }
        else if (id == 6) // chicken father
        {
            chicken.GetComponent<SpriteRenderer>().sprite = Chicken2;
            StartCoroutine("EatChicken");
        }
    }

    IEnumerator EatChicken()
    {
        Sprite s = father.GetComponent<SpriteRenderer>().sprite;
        father.GetComponent<SpriteRenderer>().sprite = fatherDage;
        yield return new WaitForSeconds(1);
        father.GetComponent<SpriteRenderer>().sprite = s;
    }

    IEnumerator SoupChange()
    {
        if (K && C && N)
        {
            yield return new WaitForSeconds(1);
            soup.GetComponent<SpriteRenderer>().sprite = SoupHalf;
            yield return new WaitForSeconds(1);
            soup.GetComponent<SpriteRenderer>().sprite = hasChicken ? SoupChickenPurple : SoupPurple;
            canClear = true;

        }
    }

    IEnumerator WaitAndDie()
    {
        soup.SetActive(false);
        KK.SetActive(false);
        CC.SetActive(false);
        NN.SetActive(false);
        father.GetComponent<SpriteRenderer>().sprite = fatherSoup;
        yield return new WaitForSeconds(2);
        father.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, -4f));
        soul.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, 8f));
        yield return new WaitForSeconds(2);
        lm.LevelClear();
        
    }

    IEnumerator WaitAndFail()
    {
        soup.SetActive(false);
        if(KK!=null)
            KK.SetActive(false);
        if(CC != null)
            CC.SetActive(false);
        if(NN != null)
            NN.SetActive(false);
        father.GetComponent<SpriteRenderer>().sprite = fatherSoup;
        yield return new WaitForSeconds(2);
        father.GetComponent<SpriteRenderer>().sprite = fatherDage;
        if (KK != null)
            KK.SetActive(true);
        if (CC != null)
            CC.SetActive(true);
        if (NN != null)
            NN.SetActive(true);
        soup.GetComponent<SpriteRenderer>().sprite = SoupEmpty;
        soup.transform.position = posSoup;
        soup.SetActive(true);
        yield return new WaitForSeconds(2);
        lm.LevelFail();

    }


}
