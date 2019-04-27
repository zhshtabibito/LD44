﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance{get; private set;}
    private int levelIndex;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
    }
    void Start()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getLevelIndex()
	{
		//用于向leveldata类传递关卡信息
		return levelIndex;
	}
    public void LevelClear()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        SaveSystem.SaveLevel(this);
        SceneManager.LoadScene(levelIndex + 1);
    }
}
