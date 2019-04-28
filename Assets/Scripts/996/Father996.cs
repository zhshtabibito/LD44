using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Father996 : MonoBehaviour
{
    public Sprite workingSprite;
    public Sprite sleepingSprite;
    public SpriteRenderer fatherRenderer;
    private bool isWorking;
    public SpriteRenderer snoozeRender;
    // Start is called before the first frame update
    void Start()
    {
        isWorking = true;
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
    }
    void OnMouseDown()
    {
        isWorking = false;
        snoozeRender.enabled = true;
        snoozeRender.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }
    public bool GetWorkingState()
    {
        return isWorking;
    }
    public void SetWorkingState(bool workingState)
    {
        isWorking = workingState;
    }
}
