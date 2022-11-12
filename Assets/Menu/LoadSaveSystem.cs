using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LoadSaveSystem
{
    const string SCORE_KEY = "score";
    public static void Save(int scoreVal)
    {
        PlayerPrefs.SetInt(SCORE_KEY, scoreVal);
    }

    public static int Load()
    {
        if (PlayerPrefs.HasKey(SCORE_KEY) == false)
        {
            int score = 0;
            if(File.Exists(Application.persistentDataPath + "gamedata.txt"))
            {
                score = int.Parse(File.ReadAllText(Application.persistentDataPath + "gamedata.txt"));
            }
            PlayerPrefs.SetInt(SCORE_KEY, score);
        }
        return PlayerPrefs.GetInt(SCORE_KEY);
    }

}
