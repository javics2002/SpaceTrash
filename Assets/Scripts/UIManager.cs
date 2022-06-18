using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clock, points;
    [SerializeField] Transform coworker, boss;
    [SerializeField] GameObject bocadilloPrefab;
    [SerializeField] Camera camera;
    [SerializeField] Spawner spawner;

    [SerializeField] string[] dialogsBoss, dialogsCoworker, specialDialoge;
    [SerializeField] float dialogTime;
    [SerializeField] Vector2 dialogOffset;
    uint dialogsCount;
    bool bossDone = false;
    RectTransform bocadilloTransform = null;

    private void Start()
    {
        InvokeRepeating("ShowText", 0, dialogTime + 0.5f);
    }

    void Update()
    {
        float time = GameManager.GetInstance().DayProgress();
        time = time / 2 + 0.333333f;

        //Convertimos el tiempo en rango 0 a 1 en horas y minutos del dia
        byte hour = (byte)(time * 24),
            minute = (byte)(time * 24 * 60 % 60);

        //Los convertimos a string para escribirlos;
        string hourString = hour.ToString(),
            minuteString = minute.ToString();

        //Les damos formato de hora
        Add0(ref hourString);
        Add0(ref minuteString);

        clock.SetText(hourString + ":" + minuteString);
        points.SetText(GameManager.GetInstance().GetScore().ToString());

        if(bocadilloTransform != null)
            bocadilloTransform.position = RectTransformUtility
                .WorldToScreenPoint(camera, bossDone ? coworker.position : boss.position) + dialogOffset;
    }

    //Añade un 0 delante del caracter, si la hora solo tiene un digito
    void Add0(ref string s)
    {
        if (s.Length == 1)
            s = "0" + s;
    }

    void ShowText()
    {
        GameObject bocadillo = Instantiate(bocadilloPrefab, transform);
        bocadillo.GetComponentInChildren<TextMeshProUGUI>()
            .SetText(bossDone ? dialogsCoworker[dialogsCount++] : dialogsBoss[dialogsCount++]);
        bocadilloTransform = bocadillo.GetComponent<RectTransform>();
        Destroy(bocadillo, dialogTime);

        if (bossDone && dialogsCount == dialogsCoworker.Length)
        {
            CancelInvoke();
            Invoke("SpawnSpecial", dialogTime * 2);
        }
        else if (!bossDone && dialogsCount == dialogsBoss.Length)
        {
            CancelInvoke();
            Invoke("BossDone", dialogTime);
        }
    }

    void BossDone()
    {
        dialogsCount = 0;
        bossDone = true;
        InvokeRepeating("ShowText", dialogTime, dialogTime + 0.5f);
    }

    void SpawnSpecial()
    {
        spawner.SpawnSpecial();
        dialogsCount = 0;
    }

    public void ShowSpecialDialogue()
    {
        GameObject bocadillo = Instantiate(bocadilloPrefab, transform);
        bocadillo.GetComponentInChildren<TextMeshProUGUI>()
            .SetText(specialDialoge[dialogsCount++]);
        bocadilloTransform = bocadillo.GetComponent<RectTransform>();
        Destroy(bocadillo, dialogTime);

        if (dialogsCount != specialDialoge.Length)
            Invoke("ShowSpecialDialogue", dialogTime);
        else if (GameManager.GetInstance().GetDay() == 3)
            GameManager.GetInstance().goodEnding = true;
    }
}
