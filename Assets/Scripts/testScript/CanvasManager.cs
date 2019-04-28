using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject clearUI;
    public GameObject failUI;
    public GameObject pauseUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetClearUI()
    {
        clearUI.SetActive(true);
        Time.timeScale = (0);
    }

    public void SetFailUI()
    {
        failUI.SetActive(true);
    }
    public void SetPauseUI()
    {
        pauseUI.SetActive(true);
    }
}
