using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clock;
    [SerializeField] Transform coworker, boss;
    [SerializeField] GameObject bocadilloPrefab;
    [SerializeField] Camera camera;

    [SerializeField] string[] dialogs;
    [SerializeField] float dialogTime;
    uint dialogsCount;
    RectTransform bocadilloTransform = null;

    private void Start()
    {
        InvokeRepeating("ShowText", 0, dialogTime + 0.5f);
    }

    void Update()
    {
        float time = GameManager.GetInstance().DayProgress();

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

        if(bocadilloTransform != null)
            bocadilloTransform.position = RectTransformUtility.WorldToScreenPoint(camera, boss.position);
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
        bocadillo.GetComponent<TextMeshProUGUI>().SetText(dialogs[dialogsCount++]);
        bocadilloTransform = bocadillo.GetComponent<RectTransform>();
        Destroy(bocadillo, dialogTime);

        if (dialogsCount == dialogs.Length)
            CancelInvoke();
    }
}
