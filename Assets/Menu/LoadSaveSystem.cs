using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LoadSaveSystem
{
    public static void Save(string score)
    {
        StreamWriter sw = new StreamWriter(GetPath());
        sw.WriteLine(score);
        sw.Close();
    }

    public static string Load()
    {
        if(File.Exists(GetPath()))
        {
            StreamReader sr = new StreamReader(GetPath());
            string result = sr.ReadLine();
            sr.Close();
            return result;
        }
        return "0";
    }

    private static string GetPath()
    {
        return Application.persistentDataPath + "gamedata.txt";
    }
}
