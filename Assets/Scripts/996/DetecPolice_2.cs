﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetecPolice_2 : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource m_audio;
    public AudioClip audioAllert;
    public Father996_2 father;
    public Police996_2 police;
    private LevelManager lm;
    void Start()
    {
        m_audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.name == "Police" && (father.GetWorkingState() == true))
        {
            StartCoroutine(WaitToFail());
        }
    }
    public IEnumerator WaitToFail()
    {
        m_audio.clip = audioAllert;
        m_audio.Play();
        police.GetComponent<AudioSource>().Stop();
        father.GetComponent<AudioSource>().Stop();
        lm = FindObjectOfType<LevelManager>();
        police.gameObject.GetComponent<Animator>().SetTrigger("Notice_Father");
        yield return new WaitForSeconds(0.1f);
        lm.LevelFail();
        Time.timeScale = 0;
    }
}
