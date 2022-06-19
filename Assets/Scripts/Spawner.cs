using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject trash;
    [SerializeField] GameObject[] specialTrash;
    [SerializeField] float spawnRate;

    void Start()
    {
        InvokeRepeating("Spawn", spawnRate, spawnRate);
    }

    void Spawn()
    {
        Instantiate(trash, transform.position, transform.rotation);
    }

    public void SpawnSpecial()
    {
        Instantiate(specialTrash[GameManager.GetInstance().GetDay() - 1], 
            transform.position, transform.rotation).GetComponentInChildren<Trash>();
    }
}
