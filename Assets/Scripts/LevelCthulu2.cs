using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCthulu2 : LevelCthuluTemplate
{
    public AudioClip AudioHeretic;
    public AudioClip AudioPolice;
    public AudioClip AudioPhone;

    public GameObject PaganFather, PaganFather2;
    public GameObject Phone, Phone2;
    public GameObject RedHeap, BlueHeap;
    public GameObject EmptyShelf;
    public GameObject GroupBubble, Bubble;
    public GameObject CthuluCalling, FatherCalling;
    public GameObject Police;
    public GameObject Heretic;
    private LevelManager lm;

    // Start is called before the first frame update
    void Start()
    {
        lm = FindObjectOfType<LevelManager>();
        state = 0;
        BlueBook.SetActive(false);
        YellowBook.SetActive(false);
        DeadBook.SetActive(false);
        RedHeap.SetActive(false);
        BlueHeap.SetActive(false);
        Magic.SetActive(false);
        GroupBubble.SetActive(false);
        Phone.SetActive(false);
        DarkBackground2.SetActive(false);
        Bubble.transform.parent = GroupBubble.transform;
        CthuluCalling.transform.parent = GroupBubble.transform;
        m_Audio = GetComponent<AudioSource>();
    }

    public override void ObjectClicked(int id, GameObject obj)
    {
        if (id == 2)
        {
            if (state < 5)
            {
                obj.GetComponent<CharacterController2D>().isClicked = false;
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
                }
                else if (state == 3)
                {
                    state = 4;
                    BlueHeap.SetActive(true);
                    Dropto(BlueHeap, -3.0f);
                }
                else if (state == 4)
                {
                    state = 5;
                    RedHeap.SetActive(true);
                    Dropto(RedHeap, -1.5f);
                    ObjectReplace(Shelf, EmptyShelf);
                    Phone.SetActive(true);
                    obj.GetComponent<CharacterController2D>().isClicked = true;
                }
                m_Audio.clip = state < 4 ? AudioDrop : AudioHeap;
                m_Audio.Play();
            }
        }
        else if (id == 3)
        {
            if(state < 6)
            {
                state = 9;
                Magic.SetActive(true);
                DeadBook.SetActive(false);
                DarkBackground2.SetActive(true);
                ObjectReplace(Father, PaganFather);
                SetMovement(Heretic, PaganFather, new Vector2(-20.0f, 0.0f), new Vector2(10.0f, 0.0f), 3.0f);
                m_Audio.clip = AudioHeretic;
                m_Audio.Play();
                StartCoroutine("WaitForRunning");
            }
        }
        else if(id == 4)
        {
            if(state == 5)
            {
                state = 10;
                ObjectReplace(Phone, Phone2);
                ObjectReplace(Father, FatherCalling);
                m_Audio.clip = AudioPhone;
                m_Audio.Play();
                StartCoroutine("WaitForReceiving");
            }
        }
    }

    IEnumerator WaitForRunning()
    {
        yield return new WaitForSeconds(3);
        SetMovement(Police, PaganFather, new Vector2(-25.0f, 0.0f), new Vector2(-2.5f, 0.0f), 3.0f);
        m_Audio.clip = AudioPolice;
        m_Audio.Play();
        state = 8;
    }

    IEnumerator WaitForCalling()
    {
        yield return new WaitForSeconds(3);
        GroupBubble.SetActive(false);
        ObjectReplace(Phone2, Phone);
        ObjectReplace(FatherCalling, Father);
        m_Audio.clip = AudioCthulu;
        m_Audio.Play();
        SetMovement(Cthulu, Father, new Vector2(-2.5f, 12.0f), new Vector2(-2.5f, 1.5f), 3.0f);
        StartCoroutine("WaitForClear");
    }

    IEnumerator WaitForReceiving()
    {
        yield return new WaitForSeconds(4);
        m_Audio.Stop();
        GroupBubble.SetActive(true);
        StartCoroutine("WaitForCalling");
    }

    IEnumerator WaitForClear()
    {
        yield return new WaitForSeconds(4);
        state = CLEAR;
    }

    IEnumerator WaitForCatch()
    {
        yield return new WaitForSeconds(4);
        ObjectReplace(PaganFather, PaganFather2);
        Magic.SetActive(false);
        DarkBackground2.SetActive(false);
        state = FAIL;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == 8)
        {
            StartCoroutine("WaitForCatch");
        }
        if (state == CLEAR)
        {
            //StartCoroutine("WaitForClear");
            lm.LevelClear();
            state = CLEAR - 1;
        }
        if(state == FAIL)
        {
            //StartCoroutine("WaitForFail");
            lm.LevelFail();
            state = FAIL + 1;
        }
    }
}
