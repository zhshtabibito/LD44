using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCafe3 : LevelSM
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
    public AudioClip audioEat;
    public AudioClip audioChickPot;

    public GameObject father;
    public GameObject soul;
    public GameObject chicken;
    public GameObject soup;

    private GameObject KK;
    private GameObject CC;
    private GameObject NN;

    private GameObject R1;
    private GameObject R2;
    private GameObject R3;

    public Sprite Chicken2;
    public Sprite fatherSoup;
    public Sprite fatherDage;

    private bool hasChicken;
    public Sprite SoupChicken;
    public Sprite SoupHalf;
    public Sprite SoupPurple;
    public Sprite SoupChickenPurple;
    public Sprite SoupEmpty;

    private bool K;
    private bool C;
    private bool N;

    public bool canClear;

    private Vector3 posSoup;

    // Start is called before the first frame update
    void Start()
    {
        canClear = false;
        posSoup = soup.transform.position;
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
        if (id == 1) // K
        {
            K = true;
            KK = obj;
            obj.GetComponent<CharacterController2D>().Target.GetComponent<BoxCollider2D>().enabled = false;
            obj.transform.parent = soup.transform;
            StartCoroutine("SoupChange");
        }
        else if (id == 2) // C
        {
            C = true;
            CC = obj;
            obj.GetComponent<CharacterController2D>().Target.GetComponent<BoxCollider2D>().enabled = false;
            obj.transform.parent = soup.transform;
            StartCoroutine("SoupChange");
        }
        else if(id == 3) // N
        {
            N = true;
            NN = obj;
            obj.GetComponent<CharacterController2D>().Target.GetComponent<BoxCollider2D>().enabled = false;
            obj.transform.parent = soup.transform;
            StartCoroutine("SoupChange");
        }
        else if(id == 4) // soup
        {
            if (canClear)
            {
                StartCoroutine("WaitAndDie");
            }
            else
            {
                StartCoroutine("WaitAndFail");
            }

        }
        else if (id == 5) // chicken pot
        {
            hasChicken = true;
            m_Audio.clip = audioChickPot;
            m_Audio.Play();
            chicken.GetComponent<SpriteRenderer>().sprite = Chicken2;
            soup.GetComponent<SpriteRenderer>().sprite = SoupChicken;

        }
        else if (id == 6) // chicken father
        {
            chicken.GetComponent<SpriteRenderer>().sprite = Chicken2;
            StartCoroutine("EatChicken");
        }
        else if (id == 7) // wrong abc
        {
            obj.GetComponent<CharacterController2D>().Target.GetComponent<BoxCollider2D>().enabled = false;
            obj.transform.parent = soup.transform;

            if (R1 != null)
                R1 = obj;
            else if (R2 != null)
                R2 = obj;
            else if (R3 != null)
                R3 = obj;
        }
    }

    IEnumerator EatChicken()
    {
        Sprite s = father.GetComponent<SpriteRenderer>().sprite;
        father.GetComponent<SpriteRenderer>().sprite = fatherDage;
        yield return new WaitForSeconds(1);
        father.GetComponent<SpriteRenderer>().sprite = s;
    }

    IEnumerator SoupChange()
    {
        if (K && C && N)
        {
            m_Audio.clip = audioWaterMix;
            m_Audio.Play();
            yield return new WaitForSeconds(0.3f);
            soup.GetComponent<SpriteRenderer>().sprite = SoupHalf;
            yield return new WaitForSeconds(0.3f);
            soup.GetComponent<SpriteRenderer>().sprite = hasChicken ? SoupChickenPurple : SoupPurple;
            canClear = true;

        }
    }

    IEnumerator WaitAndDie()
    {
        soup.SetActive(false);
        KK.SetActive(false);
        CC.SetActive(false);
        NN.SetActive(false);
        m_Audio.clip = audioDrink;
        m_Audio.Play();
        father.GetComponent<SpriteRenderer>().sprite = fatherSoup;
        yield return new WaitForSeconds(2);

        father.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, -4f));
        soul.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, 8f));
        yield return new WaitForSeconds(2);
        lm.LevelClear();
        
    }

    IEnumerator WaitAndFail()
    {
        soup.SetActive(false);
        if(KK!=null)
            KK.SetActive(false);
        if(CC != null)
            CC.SetActive(false);
        if(NN != null)
            NN.SetActive(false);
        if (R1 != null)
            R1.SetActive(false);
        if (R2 != null)
            R2.SetActive(false);
        if (R3 != null)
            R3.SetActive(false);
        m_Audio.clip = audioDrink;
        m_Audio.Play();
        father.GetComponent<SpriteRenderer>().sprite = fatherSoup;
        yield return new WaitForSeconds(2);

        father.GetComponent<SpriteRenderer>().sprite = fatherDage;
        if (KK != null)
            KK.SetActive(true);
        if (CC != null)
            CC.SetActive(true);
        if (NN != null)
            NN.SetActive(true);
        if (R1 != null)
            R1.SetActive(true);
        if (R2 != null)
            R2.SetActive(true);
        if (R3 != null)
            R3.SetActive(true);
        soup.GetComponent<SpriteRenderer>().sprite = SoupEmpty;
        soup.transform.position = posSoup;
        soup.SetActive(true);
        yield return new WaitForSeconds(2);
        lm.LevelFail();

    }


}
