using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartMenuController : MonoBehaviour
{
    public List<Button> buttons;
    private int levelPassed;
    private int loadleveldata;
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
    }

    // Start is called before the first frame update
    void Start()
    {
        loadleveldata = SaveSystem.LoadLevel();
        if(loadleveldata == 0)
        {
            for(int i = 0; i < buttons.Count; i++ )
            {
                if(i < 1)
                {
                    buttons[i].interactable = true;
                }else{
                    buttons[i].interactable = false;
                }

            }
        }else{
            levelPassed = loadleveldata;
            for(int i = 0; i < buttons.Count; i++ )
            {
                if(i < levelPassed+1)
                {
                    buttons[i].interactable = true;
                }else{
                    buttons[i].interactable = false;
                }

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel(int loadLevelIndex)
    {
        SceneManager.LoadScene(loadLevelIndex);
    }

    public void ResetLockLevel()
    {
        for(int i = 0; i < buttons.Count; i++ )
            {
                if(i < 1)
                {
                    buttons[i].interactable = true;
                }else{
                    buttons[i].interactable = false;
                }

            }
        PlayerPrefs.DeleteAll();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
