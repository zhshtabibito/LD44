using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCafe2 : LevelSM
{
    private LevelManager lm;
    private AudioSource m_Audio;
    public AudioClip audioHitGlass;
    public AudioClip audioWaterMix;
    public AudioClip audioHa;
    public AudioClip audioHitFather;
    public AudioClip audioBallFly;
    public AudioClip audioDrink;
    public AudioClip audioDie;

    public GameObject father;
    public GameObject soul;
    public GameObject mouth;
    public GameObject K;
    public GameObject r;
    public GameObject Kr;
    public GameObject K2;
    public GameObject r2;
    public GameObject K3;
    public GameObject ball;
    public Sprite fatherHitBall;
    public Sprite fatherKr;
    public Sprite fatherR;
    public Sprite fatherDage;

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
        Debug.Log(id);
        if (id == 1) // Kr
        {
            K.SetActive(false);
            r.SetActive(false);
            Kr.SetActive(true);
            StartCoroutine("WaitAndBall");
        }
        else if (id == 2) // Kr -> mouth
        {
            StartCoroutine("WaitAndDie");
        }
        else if (id == 3) // superman appear
        {
            StartCoroutine("WaitAndSuper");
        }
        else if (id == 4) // K mouth
        {
            StartCoroutine("WaitAndK");
        }
        else if (id == 5) // r mouth
        {
            StartCoroutine("WaitAndR");
        }
    }

    IEnumerator WaitAndK()
    {
        if (canClear)
        {
            m_Audio.clip = audioBallFly;
            m_Audio.Play();
            ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(6, 3));
            yield return new WaitForSeconds(0.5f);

            m_Audio.clip = audioHitFather;
            m_Audio.Play();
            father.GetComponent<SpriteRenderer>().sprite = fatherHitBall;
            ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(12, 5));
            yield return new WaitForSeconds(0.5f);

            m_Audio.clip = audioDrink;
            m_Audio.Play();
            father.GetComponent<SpriteRenderer>().sprite = fatherKr;
            K.SetActive(false);
            yield return new WaitForSeconds(2f);

            father.GetComponent<SpriteRenderer>().sprite = fatherDage;
            yield return new WaitForSeconds(2f);

            lm.LevelFail();

        }
        else
        {
            m_Audio.clip = audioBallFly;
            m_Audio.Play();
            ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(0, -2));
            yield return new WaitForSeconds(0.5f);

            m_Audio.clip = audioHitGlass;
            m_Audio.Play();
            K.SetActive(false);
            r.SetActive(false);
            r2.SetActive(true);
            K3.SetActive(true);
            ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(-12, 5));
            yield return new WaitForSeconds(2f);

            lm.LevelFail();
        }
    }

    IEnumerator WaitAndR()
    {
        if (canClear)
        {
            m_Audio.clip = audioBallFly;
            m_Audio.Play();
            ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(6, 3));
            yield return new WaitForSeconds(0.5f);

            m_Audio.clip = audioHitFather;
            m_Audio.Play();
            father.GetComponent<SpriteRenderer>().sprite = fatherHitBall;
            ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(12, 5));
            yield return new WaitForSeconds(0.5f);

            m_Audio.clip = audioDrink;
            m_Audio.Play();
            father.GetComponent<SpriteRenderer>().sprite = fatherR;
            r.SetActive(false);
            yield return new WaitForSeconds(2f);

            father.GetComponent<SpriteRenderer>().sprite = fatherDage;
            yield return new WaitForSeconds(2f);
            lm.LevelFail();
        }
        else
        {
            m_Audio.clip = audioBallFly;
            m_Audio.Play();
            ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(0, -2));
            yield return new WaitForSeconds(0.5f);

            m_Audio.clip = audioHitGlass;
            m_Audio.Play();
            K.SetActive(false);
            r.SetActive(false);
            r2.SetActive(true);
            K3.SetActive(true);
            ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(-12, 5));
            yield return new WaitForSeconds(2f);
            lm.LevelFail();
        }

    }

    IEnumerator WaitAndSuper()
    {
        yield return new WaitForSeconds(0.5f);
        m_Audio.clip = audioHa;
        m_Audio.Play();
        canClear = true;
        father.GetComponent<CharacterController2D>().MoveTo(new Vector2(father.transform.position.x, father.transform.position.y + 3));
        yield return new WaitForSeconds(0.5f);
        // mouth.GetComponent<CapsuleCollider2D>().enabled = true;
    }

    IEnumerator WaitAndBall()
    {
        if (canClear)
        {
            m_Audio.clip = audioBallFly;
            m_Audio.Play();
            ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(6, 3));
            yield return new WaitForSeconds(0.5f);

            m_Audio.clip = audioHitFather;
            m_Audio.Play();
            father.GetComponent<SpriteRenderer>().sprite = fatherHitBall;
            ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(12, 5));
            yield return new WaitForSeconds(2f);
        }
        else
        {
            m_Audio.clip = audioBallFly;
            m_Audio.Play();
            ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(0, -2));
            yield return new WaitForSeconds(0.5f);

            m_Audio.clip = audioHitGlass;
            m_Audio.Play();
            Kr.SetActive(false);
            K2.SetActive(true);
            ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(-12, 5));
            yield return new WaitForSeconds(2f);
            lm.LevelFail();
        }         
    }

    IEnumerator WaitAndDie()
    {
        m_Audio.clip = audioDrink;
        m_Audio.Play();
        Kr.SetActive(false);
        father.GetComponent<SpriteRenderer>().sprite = fatherKr;
        yield return new WaitForSeconds(2f);

        m_Audio.clip = audioDie;
        m_Audio.Play();
        father.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, -4f));
        soul.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, 8f));
        yield return new WaitForSeconds(2f);
        lm.LevelClear();  
    }

}
