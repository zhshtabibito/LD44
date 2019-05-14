using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    // Start is called before the first frame update
    Material _material;
    void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter() {
        _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, 1.0f);
    }
    private void OnMouseExit()
    {
         _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, 0.0f);
    }
}
