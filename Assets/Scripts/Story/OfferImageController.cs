using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferImageController : MonoBehaviour
{
    private LevelManager lm;
    // Start is called before the first frame update
    void Start()
    {
        lm = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        lm.LevelClear();
    }
}
