using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem 
{
    public static void SaveLevel(LevelManager levelManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/leveldata.data";

        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(levelManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static LevelData LoadLevel()
    {
        string path = Application.persistentDataPath + "/leveldata.data";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        }else
        {
            Debug.LogError("no save data in" + path);
            return null;
        }
    }
}
