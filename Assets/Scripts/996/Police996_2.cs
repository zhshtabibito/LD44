using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police996_2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float movingSpeed;
    public float movingTime;
    //public Father996 father;
    private bool towardsRight;
    private bool directionChanged;
    private float scale_x;
    void Start()
    {
        towardsRight = true;
        directionChanged = false;
        scale_x = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if(directionChanged == false)
        {
            StartCoroutine(ChangeDirection());
            directionChanged = true;
        }
        if(towardsRight == true)
        {
            gameObject.transform.localScale = new Vector3(scale_x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            GetComponent<Rigidbody2D>().velocity = new Vector3(movingSpeed, 0, 0);
        }
        if(towardsRight == false)
        {
            gameObject.transform.localScale = new Vector3(-scale_x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            GetComponent<Rigidbody2D>().velocity = new Vector3(-movingSpeed, 0, 0);
        }
    }

    public IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(movingTime);
        towardsRight = !towardsRight;
        directionChanged = false;
    }

/* 
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "DetactiveArea" && father.GetWorkingState() == true)
        {
            GetComponent<Animator>().SetTrigger("Notice_Father");
        }
    }
*/
}
