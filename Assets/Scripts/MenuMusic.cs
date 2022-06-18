using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    AudioSource music;
    
    void Start()
    {
        music = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(!music.isPlaying)
        {
            Debug.Log("Repeticion");
            OnLoop();
        }
    }

    void OnLoop()
    {
        music.time = 41.15f;
        music.Play();
    }
}
