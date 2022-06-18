using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    
    uint score;
    SortedList<uint, string>[] highscores;
    const uint maxHighscores = 10;
    string playerName;

    uint day;
    const uint maxDays = 3;
    [SerializeField] float secondsPerDay = 200;
    float secondsToday;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);

            //Leer del archivo de guardado si existe

            playerName = "Robert";
            highscores = new SortedList<uint, string>[maxDays];

            var descendingComparer = Comparer<uint>.Create((x, y) => y.CompareTo(x));

            for (int i = 0; i < highscores.Length; i++)
                highscores[i] = new SortedList<uint, string>((int) maxHighscores, descendingComparer);
        }
        else
            Destroy(this);
    }

    private void Update()
    {
        secondsToday += Time.deltaTime;

        if (secondsToday >= secondsPerDay)
            TerminaDia();
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void IniciaDia()
    {
        score = 0;
        SceneManager.LoadScene("Day" + (day + 1));
    }

    public void TerminaDia()
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

        SceneManager.LoadScene("Score");
    }

    public void AddPoints(uint points)
    {
        score += points;
    }

    public void AddPoints()
    {
        score += 100;
    }

    public uint GetScore()
    {
        return score;
    }

    public float DayProgress()
    {
        return secondsToday / secondsPerDay;
    }

    public SortedList<uint, string> GetRanking()
    {
        return highscores[day];
    }
}
