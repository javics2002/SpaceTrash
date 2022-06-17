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

    
    void Update()
    {
        
    }

    void Spawn()
    {
        Instantiate(trash);
    }
}
