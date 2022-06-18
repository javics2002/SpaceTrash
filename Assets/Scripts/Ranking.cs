using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ranking : MonoBehaviour
{
    [SerializeField] GameObject rankingPrefab;

    void Start()
    {
        SortedList<uint, string> ranking = GameManager.GetInstance().GetRanking();

        foreach (var rank in ranking)
        {
            GameObject prefab = Instantiate(rankingPrefab, transform);

            TextMeshProUGUI[] texts = prefab.GetComponentsInChildren<TextMeshProUGUI>();

            texts[0].SetText(rank.Value);
            texts[1].SetText(rank.Key.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
