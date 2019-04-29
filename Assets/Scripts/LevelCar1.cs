using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCar1 : LevelSM
{
    private LevelManager lm;
    private AudioSource m_Audio;
    public AudioClip AudioBird;
    public AudioClip AudioEgg;
    public AudioClip AudioCar;
    public AudioClip AudioAcci;
    public AudioClip AudioStop;
    public AudioClip AudioGrandma;

    public GameObject car;
    public GameObject grandma;
    public GameObject father;
    public GameObject driver1;
    public GameObject driver2;
    public GameObject driver3;
    public GameObject bird;
    public GameObject egg;
    public GameObject lightRG;


    public bool canClear;

    // Start is called before the first frame update
    void Start()
    {
        canClear = false;
        lm = FindObjectOfType<LevelManager>();
        m_Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ObjectClicked(int id, GameObject obj)
    {
        if (id == 1) // Grandma
        {
            StartCoroutine("WaitAndGreen");
        }
        else if (id == 2) // bird
        {
            m_Audio.clip = AudioBird;
            m_Audio.Play();
            Vector3 tar = grandma.transform.position + new Vector3(0, 5, 0);
            bird.GetComponent<CharacterController2D>().MoveTo(tar);
            egg.transform.position = tar;
            StartCoroutine("WaitAndEgg");
        }
    }

    IEnumerator WaitAndEgg()
    {
        yield return new WaitForSeconds(0.5f);
        egg.GetComponent<SpriteRenderer>().enabled = true;
        bird.GetComponent<CharacterController2D>().MoveSpd(new Vector2(-1, 4));
        egg.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, -3));
        StartCoroutine("WaitAndBoom");

    }

    IEnumerator WaitAndBoom()
    {
        yield return new WaitForSeconds(1.5f);
        egg.SetActive(false);
        grandma.GetComponent<SpriteRenderer>().sprite = GameObject.Find("grandma2").GetComponent<SpriteRenderer>().sprite;
        m_Audio.clip = AudioGrandma;
        m_Audio.Play();
        canClear = true;
        grandma.GetComponent<CharacterController2D>().MoveSpd(Vector2.zero);
        driver1.SetActive(false);
        driver2.SetActive(true);

    }

    IEnumerator WaitAndGreen()
    {
        yield return new WaitForSeconds(1);
        lightRG.GetComponent<SpriteRenderer>().sprite = GameObject.Find("LightG").GetComponent<SpriteRenderer>().sprite;
        StartCoroutine("WaitAndMove");
    }

    IEnumerator WaitAndMove()
    {
        m_Audio.clip = AudioCar;
        m_Audio.Play();
        yield return new WaitForSeconds(2);
        if (!canClear)
        {
            grandma.GetComponent<CharacterController2D>().MoveSpd(new Vector2(-0.5f, -0.5f));
        }
        father.GetComponent<CharacterController2D>().MoveSpd(new Vector2(-4, 0));
        car.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, -1));
        father.GetComponent<Animator>().SetTrigger("Idle2Run");
        StartCoroutine("WaitAndStop");
    }

    IEnumerator WaitAndStop()
    {
        Debug.Log("WaitAndStop");
        Debug.Log(canClear);
        yield return new WaitForSeconds(0.5f);
        if (canClear)
        {
            yield return new WaitForSeconds(0.2f);
            father.GetComponent<CharacterController2D>().MoveSpd(Vector2.zero);
            car.GetComponent<CharacterController2D>().MoveSpd(Vector2.zero);
            Debug.Log("Die");
            father.GetComponent<Animator>().SetTrigger("Run2Die");
            // clear and UI
            lm.LevelClear();
        }
        else
        {
            car.GetComponent<CharacterController2D>().MoveSpd(Vector2.zero);
            driver1.SetActive(false);
            driver3.SetActive(true);
            StartCoroutine("WaitAndFail");
        }
    }

    IEnumerator WaitAndFail()
    {
        yield return new WaitForSeconds(0.5f);
        father.GetComponent<CharacterController2D>().MoveSpd(Vector2.zero);
        father.GetComponent<Animator>().SetTrigger("Run2Idle");
        m_Audio.clip = AudioStop;
        m_Audio.Play();
        // fail and UI
        lm.LevelFail();
    }




}
