using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCafe1 : LevelSM
{
    private LevelManager lm;
    private AudioSource m_Audio;
    public AudioClip audioHitGlass;
    public AudioClip audioDrinking;
    public AudioClip audioBallFly;
    public AudioClip audioMix;

    public GameObject father;
    public GameObject soul;
    public GameObject juice;
    public GameObject KCN;
    public GameObject juice2;
    public GameObject KCN2;
    public GameObject juice3;
    private Sprite fatherOri;
    public Sprite fatherDrinkKCN;
    public Sprite fatherDrinkJuice;
    public GameObject ball;

    public bool canClear;

    // Start is called before the first frame update
    void Start()
    {
        fatherOri = father.GetComponent<SpriteRenderer>().sprite;

        canClear = false;
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
        if (id == 1) // KCN -> juice
        {
            m_Audio.clip = audioMix;
            m_Audio.Play();
            KCN.SetActive(false);
            KCN2.SetActive(true);
        }
        else if (id == 2) // KCN -> mouth
        {     
            KCN.SetActive(false);
            juice.GetComponent<CapsuleCollider2D>().enabled = false;
            father.GetComponent<SpriteRenderer>().sprite = fatherDrinkKCN;
            StartCoroutine("WaitAndDie");
        }
        else if(id == 3) // juice
        {
            juice.SetActive(false);
            father.GetComponent<SpriteRenderer>().sprite = fatherDrinkJuice;
            StartCoroutine("WaitAndBall");
        }
    }

    IEnumerator WaitAndDie()
    {
        m_Audio.clip = audioDrinking;
        m_Audio.Play();
        yield return new WaitForSeconds(2f);

        father.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, -4f));
        soul.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, 8f));
        yield return new WaitForSeconds(1f);

        m_Audio.clip = audioBallFly;
        m_Audio.Play();
        ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(0, -2));
        yield return new WaitForSeconds(0.5f);

        m_Audio.clip = audioHitGlass;
        m_Audio.Play();
        juice.SetActive(false);
        juice2.SetActive(true);
        ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(-12, 5));
        yield return new WaitForSeconds(2f);

        lm.LevelClear();
        
    }

    IEnumerator WaitAndBall()
    {
        m_Audio.clip = audioDrinking;
        m_Audio.Play();
        yield return new WaitForSeconds(1f);

        m_Audio.clip = audioBallFly;
        m_Audio.Play();
        ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(0, -2));
        yield return new WaitForSeconds(0.3f);

        m_Audio.clip = audioHitGlass;
        m_Audio.Play();
        juice3.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        KCN.SetActive(false);
        KCN2.SetActive(true);
        yield return new WaitForSeconds(0.5f);   

        father.GetComponent<SpriteRenderer>().sprite = fatherOri;
        juice3.SetActive(false);
        juice2.SetActive(true);
        ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(-12, 5));
        yield return new WaitForSeconds(2f);

        lm.LevelFail();

    }


}
