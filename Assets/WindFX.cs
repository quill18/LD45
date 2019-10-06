using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindFX : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    LevelManager levelManager;
    AudioSource src;

    // Update is called once per frame
    void Update()
    {
        if (levelManager.isOutdoors() && src.isPlaying == false)
        {
            src.Play();
        }
        else if(levelManager.isOutdoors() == false)
        {
            src.Stop();
        }
            
    }
}
