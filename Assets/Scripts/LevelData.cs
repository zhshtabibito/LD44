using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//表示这个文件可以被存储
public class LevelData 
{
    public int levelIndex = 0;
    public LevelData(LevelManager lm)
    {
        levelIndex = lm.getLevelIndex();
    }
}
