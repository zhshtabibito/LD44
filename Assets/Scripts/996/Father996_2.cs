using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Father996_2 : MonoBehaviour
{
    public Sprite workingSprite;
    public Sprite sleepingSprite;
    public Sprite deadSprite;
    public SpriteRenderer fatherRenderer;
    private bool isWorking;
    private bool isDead;
    public Snooze996_2 snooze;
    public SpriteRenderer soul;
    public GameObject detector;
    // Start is called before the first frame update
    void Start()
    {
        isWorking = true;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if(isWorking == true)
        {
            fatherRenderer.sprite = workingSprite;
        }
        if(isWorking == false)
        {
            fatherRenderer.sprite = sleepingSprite;
        }
        if(isDead == true)
        {
            fatherRenderer.sprite = deadSprite;
        }
    }
    void OnMouseDown()
    {
        isWorking = false;
        snooze.EnableSnooze();
    }
    public bool GetWorkingState()
    {
        return isWorking;
    }
    public void SetWorkingState(bool workingState)
    {
        isWorking = workingState;
    }
    public void Dead()
    {
        isDead = true;
        soul.enabled = true;
        detector.GetComponent<BoxCollider2D>().enabled = false;
    }
}
