using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpaceTrashSave
{
    public string playerName;
    public string[,] names;
    public uint[,] points;

    public SpaceTrashSave (GameManager gm)
    {
        playerName = gm.GetName();
        names = new string[3, 10];
        points = new uint[3, 10];

        SortedList<uint, string>[] highscores = gm.GetHighscores();
        for (int i = 0; i < 3; i++)
            for(int j = 0; j < 10; j++)
            {
                if(highscores[i].Values.Count > j)
                {
                    names[i, j] = highscores[i].Values[j];
                    points[i, j] = highscores[i].Keys[j];

                    Debug.Log("Guardado dia " + i + ":" + names[i, j] + points[i, j].ToString());
                }
                else
                {
                    names[i, j] = "a";
                    points[i, j] = 0;
                }
            }
    }

    public SortedList<uint, string>[] GetHighscores()
    {
        SortedList<uint, string>[] list = new SortedList<uint, string>[3];

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 10; j++)
                if (points[i,j] > 0)
                {
                    Debug.Log("Recuperado dia " + i + ":" + names[i, j] + points[i, j].ToString());
                    list[i].Add(points[i, j], names[i, j]);
                }else
                    Debug.Log("Nada en dia " + i + j);
        return list;
    }
}
