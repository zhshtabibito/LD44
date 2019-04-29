using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offerSM : LevelSM
{
    private float alpha1;
    private float alpha2;
    private float alpha3;
    private float alpha4;
    private float alpha5;
    private float alpha6;
    private int state;
    private bool trigger;
    public GameObject offer;
    public GameObject offerImage;

    // Start is called before the first frame update
    void Start()
    {
        alpha1 = 1;
        alpha2 = 1;
        alpha3 = 1;
        alpha4 = 1;
        alpha5 = 0;
        alpha6 = 0;
        state = 0;
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == 4 && trigger == false)
        {
            offer.SetActive(true);
            StartCoroutine(FadeIn(offer, alpha5));
            trigger = true;
        }
    }

    public override void ObjectClicked(int id, GameObject obj)
    {
        if(id == 1)
        {
            StartCoroutine(FadeOut(obj, alpha1));
            state++;
        }
        if(id == 2)
        {
            StartCoroutine(FadeOut(obj, alpha2));
            state++;
        }
        if(id == 3)
        {
            StartCoroutine(FadeOut(obj, alpha3));
            state++;
        }
        if(id == 4)
        {
            StartCoroutine(FadeOut(obj, alpha4));
            state++;
        }
        if(id == 5)
        {
            //Debug.Log("clicking offer");
            obj.SetActive(false);
            offerImage.SetActive(true);
            StartCoroutine(FadeIn(offerImage, alpha6));
        }
    }

    public IEnumerator FadeOut(GameObject obj, float alpha)
    {
        while(alpha >= 0)
        {
            alpha -= 0.03f;
            obj.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator FadeIn(GameObject obj, float alpha)
    {
        while(alpha <= 1)
        {
            alpha += 0.03f;
            obj.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
            yield return new WaitForFixedUpdate();
        }
    }
}
