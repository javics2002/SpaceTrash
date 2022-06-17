using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    
    uint score;
    SortedList<uint, string>[] highscores;
    const uint maxHighscores = 10;
    string playerName;

    [SerializeField] GameObject rankingPrefab;

    uint day;
    const uint maxDays = 3;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);

            //Leer del archivo de guardado si existe

            playerName = "Robert";
            highscores = new SortedList<uint, string>[maxDays];
        }
        else
            Destroy(this);
    }

    public static GameManager getInstance()
    {
        return instance;
    }

    void IniciaDia()
    {
        score = 0;
    }

    void TerminaDia()
    {
        while (score > 0)
        {
            try
            {
                //Añade la puntuacion a la tabla
                highscores[day].Add(score, playerName);
                break;
            }
            catch (ArgumentException)
            {
                //Si ya alguien tiene la misma puntuacion, se le resta 1 para estar por debajo
                score--;
            }
        }

        //Solo guardo 10 records
        if (highscores[day].Count > maxHighscores)
            highscores[day].RemoveAt((int) maxHighscores);
    }

    void AddPoints(uint points)
    {
        score += points;
    }
}
