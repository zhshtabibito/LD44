using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCafe3 : LevelSM
{
    private LevelManager lm;
    private AudioSource m_Audio;

    public GameObject father;
    public GameObject soul;
    public GameObject chicken;
    public GameObject soup;

    private GameObject KK;
    private GameObject CC;
    private GameObject NN;

    private bool K;
    private bool C;
    private bool N;

    public bool canClear;

    // Start is called before the first frame update
    void Start()
    {
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
        else if (id == 5) // chicken
        {
            chicken.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Chicken2").GetComponent<SpriteRenderer>().sprite;

        }
    }

    IEnumerator SoupChange()
    {
        if (K && C && N)
        {
            yield return new WaitForSeconds(1);
            soup.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Soup2").GetComponent<SpriteRenderer>().sprite;
            yield return new WaitForSeconds(1);
            soup.GetComponent<SpriteRenderer>().sprite = GameObject.Find("Soup3").GetComponent<SpriteRenderer>().sprite;
            canClear = true;

        }
    }

    IEnumerator WaitAndDie()
    {
        soup.SetActive(false);
        KK.SetActive(false);
        CC.SetActive(false);
        NN.SetActive(false);
        father.GetComponent<SpriteRenderer>().sprite = GameObject.Find("FatherSoup").GetComponent<SpriteRenderer>().sprite;
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
        Sprite s = father.GetComponent<SpriteRenderer>().sprite;
        father.GetComponent<SpriteRenderer>().sprite = GameObject.Find("FatherSoup").GetComponent<SpriteRenderer>().sprite;
        yield return new WaitForSeconds(2);
        father.GetComponent<SpriteRenderer>().sprite = s;
        yield return new WaitForSeconds(2);
        lm.LevelFail();

    }


}
