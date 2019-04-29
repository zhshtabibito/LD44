using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferSceneManager : MonoBehaviour
{
    public List<Sprite> Frames;
    public float frameInterval;
    public SpriteRenderer SR;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAnimation());
        //SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartAnimation()
    {
        
        foreach(Sprite frame in Frames)
        {
            SR.sprite = frame;
            yield return new WaitForSeconds(frameInterval);
        }
        gameObject.SetActive(false);   
    }
}
