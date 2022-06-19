using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

    public bool goodEnding = false, badEnding = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);

            //Leer del archivo de guardado si existe
            /*SpaceTrashSave save = LoadData();

            if(save != null)
            {
                Debug.Log("Archivo encontrado");
                playerName = save.playerName;
                highscores = save.GetHighscores();
            }
            else*/
            {
                playerName = Environment.UserName;
                highscores = new SortedList<uint, string>[maxDays];
            }

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
        {
            secondsToday = 0;
            TerminaDia();
        }
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void IniciaDia()
    {
        day++;
        score = 0;

        if(day > maxDays)
        {
            day = 0;

            if(badEnding)
                SceneManager.LoadScene("BadEnding");
            else if(goodEnding)
                SceneManager.LoadScene("GoodEnding");
            else
                SceneManager.LoadScene("End");

            goodEnding = badEnding = false;
        }
        else
            SceneManager.LoadScene("Day" + day);
    }

    public void IniciaDia(uint day)
    {
        score = 0;
        secondsToday = 0;
        SceneManager.LoadScene(day >= maxDays ? "End" : "Day" + day);
    }

    public void TerminaDia()
    {
        while (score > 0)
        {
            try
            {
                //Añade la puntuacion a la tabla
                highscores[day - 1].Add(score, playerName);
                break;
            }
            catch (ArgumentException)
            {
                //Si ya alguien tiene la misma puntuacion, se le resta 1 para estar por debajo
                score--;
            }
        }

        //Solo guardo 10 records
        if (highscores[day - 1].Count > maxHighscores)
            highscores[day - 1].RemoveAt((int) maxHighscores);

        SceneManager.LoadScene("Score");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
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

    public uint GetDay()
    {
        return day;
    }

    public float DayProgress()
    {
        return secondsToday / secondsPerDay;
    }

    public SortedList<uint, string> GetRanking()
    {
        return highscores[day - 1];
    }
    
    public string GetName()
    {
        return playerName;
    }

    public SortedList<uint, string>[] GetHighscores()
    {
        return highscores;
    }

    public void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.sps";
        FileStream stream = new(path, FileMode.Create);

        SpaceTrashSave data = new(this);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public SpaceTrashSave LoadData()
    {
        string path = Application.persistentDataPath + "/save.sps";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SpaceTrashSave data = formatter.Deserialize(stream) as SpaceTrashSave;
            stream.Close();

            return data;
        }
        else
            return null;
    }
}
