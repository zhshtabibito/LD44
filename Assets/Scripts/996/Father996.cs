using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Father996 : MonoBehaviour
{
    public AudioClip audioWorking;
    public AudioClip audioSleeping;
    public AudioClip audioDead;
    private AudioSource m_audio;
    private bool audioChange;

    public Sprite workingSprite;
    public Sprite sleepingSprite;
    public Sprite deadSprite;
    public SpriteRenderer fatherRenderer;
    private bool isWorking;
    private bool isDead;
    public Snooze996 snooze;
    public SpriteRenderer soul;
    public GameObject detector;
    // Start is called before the first frame update
    void Start()
    {
        isWorking = true;
        isDead = false;
        audioChange = false;
        m_audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if(isWorking == true)
        {
            if(audioChange == true)
            {
                m_audio.clip = audioWorking;
                m_audio.Play();
                audioChange = false;
            }
            fatherRenderer.sprite = workingSprite;
        }
        if(isWorking == false)
        {
            if(audioChange == true)
            {
                m_audio.clip = audioSleeping;
                m_audio.Play();
                audioChange = false;
            }
            fatherRenderer.sprite = sleepingSprite;
        }
        if(isDead == true )
        {
            if(audioChange == true)
            {
                m_audio.Stop();
                m_audio.clip = audioDead;
                m_audio.loop = false;
                m_audio.Play();
                audioChange = false;
            }
            fatherRenderer.sprite = deadSprite;
        }
    }
    void OnMouseDown()
    {
        audioChange = true;
        isWorking = false;
        snooze.EnableSnooze();
    }
    public bool GetWorkingState()
    {
        return isWorking;
    }
    public void SetWorkingState(bool workingState)
    {
        audioChange = true;
        isWorking = workingState;
    }
    public void Dead()
    {
        m_audio.Stop();
        m_audio.clip = audioDead;
        m_audio.loop = false;
        m_audio.Play();
        isDead = true;
        soul.enabled = true;
        detector.GetComponent<BoxCollider2D>().enabled = false;
    }
}
