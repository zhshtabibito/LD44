using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetecPolice : MonoBehaviour
{
    private AudioSource m_audio;
    public AudioClip audioAllert;
    
    public Father996 father;
    public Police996 police;
    private LevelManager lm;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_audio = GetComponent<AudioSource>();
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
        lm = FindObjectOfType<LevelManager>();
        police.gameObject.GetComponent<Animator>().SetTrigger("Notice_Father");
        yield return new WaitForSeconds(0.1f);
        lm.LevelFail();
        Time.timeScale = 0;
    }
}
