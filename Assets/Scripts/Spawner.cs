using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject trash;
    [SerializeField] float spawnRate;

    void Start()
    {
        InvokeRepeating("Spawn", spawnRate, spawnRate);
    }

    void Spawn()
    {
        Instantiate(trash, transform.position, transform.rotation);
    }
}
