using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveSystem 
{
    
    public static void SaveLevel(int levelIndex)
    {
        if(levelIndex > PlayerPrefs.GetInt("maxLevel"))
        {
            PlayerPrefs.SetInt("maxLevel", levelIndex);
            Debug.Log("saved level:" + levelIndex);
        }
    }

    public static int LoadLevel()
    {
        int data = PlayerPrefs.GetInt("maxLevel");
        Debug.Log("loaded level:" + PlayerPrefs.GetInt("maxLevel"));
        return data;
    }
}
