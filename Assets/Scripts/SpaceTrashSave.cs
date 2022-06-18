using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTrashSave : MonoBehaviour
{
    public string playerName;
    public SortedList<uint, string>[] highscores;

    public SpaceTrashSave (GameManager gm)
    {
        playerName = gm.GetName();
        highscores = gm.GetHighscores();
    }
}
