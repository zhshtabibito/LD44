using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCthulu3 : LevelCthuluTemplate
{
    public List<GameObject> LettersFixed, Letters, LettersDark;
    public GameObject TED;
    public GameObject Carpet;
    public GameObject PaganFather;
    public GameObject DarkBackground;
    public GameObject Light;
    private LevelManager lm;
    private int flag, failflag;

    private int set1(int bitmap, int dig)
    {
        bitmap |= 1 << dig;
        return bitmap;
    }

    private int set0(int bitmap, int dig)
    {
        bitmap &= (~(1 << dig));
        return bitmap;
    }

    private bool eq0(int bitmap, int dig)
    {
        return (bitmap & (1 << dig)) == 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        lm = FindObjectOfType<LevelManager>();
        string[] LFArr = { "C1", "T1", "H", "U1", "L", "U2" };
        string[] LArr = { "E1", "X", "C2", "E2", "P", "T2", "E3", "D" };
        string[] LDArr = { "E1D", "XD", "C2D", "E2D", "PD", "T2D", "E3D", "DD" };
        LettersFixed = new List<GameObject>();
        Letters = new List<GameObject>();
        LettersDark = new List<GameObject>();
        state = 0;
        BlueBook.SetActive(false);
        YellowBook.SetActive(false);
        DeadBook.SetActive(false);
        TED.SetActive(false);
        DarkBackground.SetActive(false);
        //Carpet.SetActive(false);
        //Light.SetActive(false);
        //DarkBackground2.SetActive(false);
        for (int i = 2; i < 8; i++)
            flag += (1 << i);
        for (int i = 2; i < 11; i++)
            failflag += (1 << i);

        foreach (string name in LDArr)
        {
            GameObject obj = GameObject.Find(name);
            LettersDark.Add(obj);
            obj.SetActive(false);
        }
        foreach (string name in LFArr)
        {
            GameObject obj = GameObject.Find(name);
            LettersFixed.Add(obj);
            obj.SetActive(false);
        }
        foreach (string name in LArr)
        {
            GameObject obj = GameObject.Find(name);
            Letters.Add(obj);
            obj.SetActive(false);
        }
    }

    public override void ObjectClicked(int id, GameObject Obj)
    {
        if (id == 1)
        {
            if (state < 5)
            {
                Obj.GetComponent<CharacterController2D>().isClicked = false;
                if (state == 0)
                {
                    state = 1;
                    BlueBook.SetActive(true);
                    Dropto(BlueBook, -3.2f);
                }
                else if (state == 1)
                {
                    state = 2;
                    YellowBook.SetActive(true);
                    Dropto(YellowBook, -3.2f);
                }
                else if (state == 2)
                {
                    state = 3;
                    DeadBook.SetActive(true);
                    Dropto(DeadBook, -3.2f);
                    Obj.GetComponent<CharacterController2D>().isClicked = true;
                }
            }
        }
        else if (id == 2)
        {
            if (state == 3)
            {
                foreach(GameObject obj in Letters)
                {
                    obj.SetActive(true);
                }
                foreach (GameObject obj in LettersFixed)
                {
                    obj.SetActive(true);
                }
                state = 4;
            }
        }
        else if(id > 2 && id < 11)
        {
            if(state >= 4)
            {
                int idx = id - 3;
                if(eq0(state, id))
                {
                    state = set1(state, id);
                    Letters[idx].SetActive(false);
                    LettersDark[idx].SetActive(true);
                }
                /*
                else
                {
                    state = set0(state, id);
                    Letters[idx].SetActive(true);
                    LettersDark[idx].SetActive(false);
                }
                */
                //Debug.Log(state);
                if (state == flag)
                {
                    TED.SetActive(true);
                    //Carpet.SetActive(true);
                    //Light.SetActive(true);
                    //Background.SetActive(false);
                    //DarkBackground2.SetActive(true);
                    foreach (GameObject obj in Letters)
                    {
                        obj.SetActive(false);
                    }
                    foreach (GameObject obj in LettersFixed)
                    {
                        obj.SetActive(false);
                    }
                    foreach (GameObject obj in LettersDark)
                    {
                        obj.SetActive(false);
                    }
                    StartCoroutine("WaitForComeDown");
                }
                else if (state == failflag)
                {
                    StartCoroutine("WaitForFail");
                }

            }
        }
    }

    IEnumerator WaitForFail()
    {
        yield return new WaitForSeconds(1);
        state = FAIL;
    }

    IEnumerator WaitForClear()
    {
        yield return new WaitForSeconds(2);
        state = CLEAR;
    }

    IEnumerator WaitForComeDown()
    {
        SetMovement(Cthulu, Father, new Vector2(0.0f, 20.0f), new Vector2(0.0f, 2.0f), 4.0f);
        yield return new WaitForSeconds(3.6f);
        ObjectReplace(Father, PaganFather);
        StartCoroutine("WaitForClear");
    }

    // Update is called once per frame
    void Update()
    {
        if (state == CLEAR)
        {
            //StartCoroutine("WaitForClear");
            lm.LevelClear();
            state = CLEAR - 1;
        }
        if(state == FAIL)
        {
            //StartCoroutine("WaitForFail", lm);
            lm.LevelFail();
            state = FAIL + 1;
        }
    }
}
