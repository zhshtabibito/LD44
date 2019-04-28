using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetecPolice : MonoBehaviour
{
    // Start is called before the first frame update
    public Father996 father;
    private LevelManager lm;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.name == "Police" && (father.GetWorkingState() == true))
        {
            lm = FindObjectOfType<LevelManager>();
            lm.LevelFail();
            Time.timeScale = 0;
        }
    }
}
