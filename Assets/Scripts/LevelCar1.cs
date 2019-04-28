using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCar1 : LevelSM
{
    public GameObject car;
    public GameObject grandma;
    public GameObject father;
    public GameObject bird;
    public GameObject egg;
    public GameObject lightRG;


    public bool canClear;

    // Start is called before the first frame update
    void Start()
    {
        canClear = false;
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
            Vector3 tar = grandma.transform.position + new Vector3(0, 5, 0);
            bird.GetComponent<CharacterController2D>().MoveTo(tar);
            egg.transform.position = tar;
            StartCoroutine("WaitAndEgg");



        }






    }

    IEnumerator WaitAndEgg()
    {
        yield return new WaitForSeconds(1f);
        egg.GetComponent<SpriteRenderer>().enabled = true;
        egg.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, -3f));
        StartCoroutine("WaitAndBoom");

    }

    IEnumerator WaitAndBoom()
    {
        yield return new WaitForSeconds(1f);
        egg.SetActive(false);
        grandma.GetComponent<SpriteRenderer>().sprite = GameObject.Find("grandma2").GetComponent<SpriteRenderer>().sprite;
        canClear = true;
        grandma.GetComponent<CharacterController2D>().MoveSpd(Vector2.zero);
    }

    IEnumerator WaitAndGreen()
    {
        yield return new WaitForSeconds(2);
        lightRG.GetComponent<SpriteRenderer>().sprite = GameObject.Find("LightG").GetComponent<SpriteRenderer>().sprite;
        StartCoroutine("WaitAndMove");
    }

    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(2);
        grandma.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, -0.5f));
        father.GetComponent<CharacterController2D>().MoveSpd(new Vector2(-2, 0));
        car.GetComponent<CharacterController2D>().MoveSpd(new Vector2(0, -2));
        father.GetComponent<Animator>().SetTrigger("Idle2Run");
        StartCoroutine("WaitAndStop");
    }

    IEnumerator WaitAndStop()
    {
        Debug.Log("WaitAndStop");
        Debug.Log(canClear);
        yield return new WaitForSeconds(2);
        if (canClear)
        {
            yield return new WaitForSeconds(1);
            father.GetComponent<CharacterController2D>().MoveSpd(Vector2.zero);
            car.GetComponent<CharacterController2D>().MoveSpd(Vector2.zero);
            Debug.Log("Die");
            father.GetComponent<Animator>().SetTrigger("Run2Die");
            // TODO: clear and UI
        }
        else
        {
            car.GetComponent<CharacterController2D>().MoveSpd(Vector2.zero);
            StartCoroutine("WaitAndFail");
        }
    }

    IEnumerator WaitAndFail()
    {
        yield return new WaitForSeconds(1);
        father.GetComponent<CharacterController2D>().MoveSpd(Vector2.zero);
        father.GetComponent<Animator>().SetTrigger("Run2Idle");
        // TODO: fail and UI
    }




}
