using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCafe2 : LevelSM
{
    private LevelManager lm;
    private AudioSource m_Audio;

    public GameObject father;
    public GameObject soul;
    public GameObject K;
    public GameObject r;
    public GameObject Kr;
    public GameObject K2;
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
        if (id == 1) // Kr
        {
            K.SetActive(false);
            r.SetActive(false);
            Kr.SetActive(true);
        }
        else if (id == 2) // Kr -> mouth
        {
            if (canClear)
            {
                StartCoroutine("WaitAndDie");
            }
            else
            {
                StartCoroutine("WaitAndBall");
            }
        }
        else if(id == 3) // superman appear
        {
            StartCoroutine("WaitAndSuper");
        }
    }

    IEnumerator WaitAndSuper()
    {
        yield return new WaitForSeconds(0.5f);
        canClear = true;
        father.GetComponent<CharacterController2D>().MoveTo(new Vector2(father.transform.position.x, father.transform.position.y + 3));
    }

    IEnumerator WaitAndBall()
    {
        ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(0, -2));
        yield return new WaitForSeconds(0.5f);
        Kr.SetActive(false);
        K2.SetActive(true);
        ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(-12, 5));
        yield return new WaitForSeconds(2f);
        lm.LevelFail();

    }

    IEnumerator WaitAndDie()
    {

        ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(6, 3));
        yield return new WaitForSeconds(0.5f);
        ball.GetComponent<CharacterController2D>().MoveTo(new Vector2(12, 5));
        yield return new WaitForSeconds(2f);

        Kr.SetActive(false);
        K2.SetActive(true);

        yield return new WaitForSeconds(2f);
        father.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, -4f));
        soul.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, 8f));

        yield return new WaitForSeconds(2f);
        lm.LevelClear();
      
    }

}
