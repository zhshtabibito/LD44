﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snooze996_2 : MonoBehaviour
{
    
    public AudioClip AudioBroken;
    private AudioSource m_audio;
    // Start is called before the first frame update
    private Animator snoozeAnimator;
    public float waitTime;
    public GameObject father;
    public float biggerTime;
    private IEnumerator cR;
    //private SpriteRenderer sR;
    //泡泡破碎后停留多久
    void Start()
    {
        snoozeAnimator = GetComponent<Animator>();
        cR = SnoozeChange();
        m_audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void   OnMouseDown() {
        if(GetComponent<Animator>().GetBool("Snooze_Bigger") == false)
        {
            snoozeAnimator.SetBool("Snooze_Broken1", true);
            m_audio.clip = AudioBroken;
            m_audio.Play();
            StartCoroutine(WaitToDisable());
            StopCoroutine("SnoozeChange");
            Debug.Log("snooze broken 1");
        }else{
            StartCoroutine(BiggerBroken());
        }
        
    }

    public IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        snoozeAnimator.SetBool("Snooze_Broken1", false);
        father.GetComponent<Father996_2>().SetWorkingState(true);
        //GetComponent<Snooze996_2>().enabled = false;
    }

    public void EnableSnooze()
    {
        //GetComponent<Snooze996_2>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CapsuleCollider2D>().enabled = true;
        StartCoroutine("SnoozeChange");
    }

    public IEnumerator SnoozeChange()
    {
        yield return new WaitForSeconds(biggerTime);
        snoozeAnimator.SetBool("Snooze_Bigger", true);
    }
    public IEnumerator BiggerBroken()
    {
        snoozeAnimator.SetTrigger("Snooze_Broken2");
        m_audio.clip = AudioBroken;
        m_audio.Play();
        yield return new WaitForSeconds(1.0f);

        GetComponent<SpriteRenderer>().enabled = false;
        father.GetComponent<Father996_2>().Dead();
        yield return new WaitForSeconds(3.0f);

        FindObjectOfType<LevelManager>().LevelClear();
    }
}