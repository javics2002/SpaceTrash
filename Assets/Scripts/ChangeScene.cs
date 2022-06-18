using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void Continue()
    {
        GameManager.GetInstance().IniciaDia();
    }

    public void TerminaDia()
    {
        GameManager.GetInstance().TerminaDia();
    }

    public void SumaPuntos()
    {
        GameManager.GetInstance().AddPoints(100);
    }

    public void Salir()
    {
        GameManager.GetInstance().SaveData();
        Application.Quit();
    }
}
