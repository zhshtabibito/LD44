using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailLevel : MonoBehaviour
{
    private LevelManager lm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        lm = FindObjectOfType<LevelManager>();
        lm.LevelFail();
    }
}
