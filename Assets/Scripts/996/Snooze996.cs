using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snooze996 : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator snoozeAnimator;
    public float waitTime;
    public GameObject father;
    //泡泡破碎后停留多久
    void Start()
    {
        snoozeAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void   OnMouseDown() {
        snoozeAnimator.SetBool("Snooze_Broken", true);
        StartCoroutine(WaitToDisable());
        
    }

    public IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        snoozeAnimator.SetBool("Snooze_Broken", false);
        father.GetComponent<Father996>().SetWorkingState(true);
    }
}
