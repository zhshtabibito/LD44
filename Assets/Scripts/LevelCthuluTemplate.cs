using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class LevelCthuluTemplate : LevelSM
{
    protected int state;
    protected const int CLEAR = 0x7fffffff;
    protected const int FAIL = -0x7fffffff;

    public GameObject Background;
    public GameObject Magic;
    public GameObject Father;
    public GameObject Cthulu;
    public GameObject YellowBook;
    public GameObject Shelf;
    public GameObject BlueBook;
    public GameObject DeadBook;
    public GameObject DarkBackground2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected void ObjectReplace(GameObject src, GameObject des)
    {
        Vector3 pos = src.transform.position;
        des.transform.position = pos;
        src.SetActive(false);
        des.SetActive(true);
    }

    protected void Dropto(GameObject obj, float y)
    {
        Vector3 pos = obj.transform.position;
        obj.GetComponent<CharacterController2D>().MoveTo(new Vector2(pos.x, y));
    }

    protected void SetMovement(GameObject obj, GameObject target, Vector2 startoffset, Vector2 endoffset, float time)
    {
        Vector2 tpos = target.transform.position;
        Vector2 start = tpos + startoffset;
        obj.GetComponent<Rigidbody2D>().position = start;
        obj.GetComponent<CharacterController2D>().MoveTo(tpos + endoffset, time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    protected IEnumerator WaitForClear()
    {
        yield return new WaitForSeconds(4);
        Debug.Log("Level Clear\n");
    }

    protected IEnumerator WaitForFail()
    {
        yield return new WaitForSeconds(4);
        Debug.Log("Level Fail\n");
    }
    */
}
