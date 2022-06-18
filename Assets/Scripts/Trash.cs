using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] Sprite[] organica, minerales, tecnologica, radiactiva, especial;
    public enum TrashType { organic, mineral, techno, radioactive, special };

    TrashType type;

    void Start()
    {
        type = (TrashType) Random.Range(0, GameManager.GetInstance().GetDay() + 3);

        GetComponent<SpriteRenderer>().sprite = RandomTrash();
    }

    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }

    Sprite RandomTrash() => type switch
    {
        TrashType.organic => organica[Random.Range(0, organica.Length)],
        TrashType.mineral => minerales[Random.Range(0, minerales.Length)],
        TrashType.techno => tecnologica[Random.Range(0, tecnologica.Length)],
        TrashType.radioactive => radiactiva[Random.Range(0, radiactiva.Length)]
    };

    void SetSpecial(uint index)
    {
        type = TrashType.special;
        GetComponent<SpriteRenderer>().sprite = especial[index];
    }

    public bool IsType(TrashType t)
    {
        return t == type;
    }
}
