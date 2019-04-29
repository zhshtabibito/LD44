using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCafe1 : LevelSM
{
    private LevelManager lm;
    private AudioSource m_Audio;

    public GameObject father;
    public GameObject soul;
    public GameObject juice;
    public GameObject KCN;
    public GameObject juice2;
    public GameObject KCN2;
    public GameObject ball;

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
        if (id == 1) // KCN -> juice
        {
            KCN.SetActive(false);
            KCN2.SetActive(true);
        }
        else if (id == 2) // KCN -> mouth
        {     
            KCN.SetActive(false);
            KCN2.SetActive(true);
            StartCoroutine("WaitAndDie");
        }
        else if(id == 3) // juice
        {
            StartCoroutine("WaitAndBall");
        }
    }

    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(2f);
        father.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, -4f));
        soul.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, 8f));

        yield return new WaitForSeconds(1f);
        ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(0, -2));
        yield return new WaitForSeconds(0.5f);
        juice.SetActive(false);
        juice2.SetActive(true);
        ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(-12, 5));
        yield return new WaitForSeconds(2f);
        lm.LevelClear();
        
    }

    IEnumerator WaitAndBall()
    {
        yield return new WaitForSeconds(1f);
        ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(0, -2));
        yield return new WaitForSeconds(0.5f);
        KCN.SetActive(false);
        KCN2.SetActive(true);
        juice.SetActive(false);
        juice2.SetActive(true);
        ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(-12, 5));
        yield return new WaitForSeconds(2f);
        lm.LevelFail();

    }


}
