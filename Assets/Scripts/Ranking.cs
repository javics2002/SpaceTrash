using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    [SerializeField] GameObject rankingPrefab;

    void Start()
    {
        SortedList<uint, string> ranking = new SortedList<uint, string>
        {
            { 2000, "cano" },
            { 2020, "pablo" },
            { 2300, "belchi" },
            { 2050, "david" },
            { 2550, "jorge" }
        };// = GameManager.GetInstance().GetRanking();

        foreach (var rank in ranking)
        {
            Instantiate(rankingPrefab, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
