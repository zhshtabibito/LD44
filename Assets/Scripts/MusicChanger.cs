using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public AudioClip BGM3;
    private LevelManager lm;

    // Start is called before the first frame update
    void Start()
    {
        lm = FindObjectOfType<LevelManager>();
        lm.GetComponent<AudioSource>().clip = BGM3;
        lm.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
