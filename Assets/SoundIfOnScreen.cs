using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundIfOnScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        src = GetComponent<AudioSource>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();

        nextSound = RandomSoundDelay / 2f;
    }

    LevelManager levelManager;
    AudioSource src;
    SpriteRenderer sr;

    public AudioClip[] RandomSounds;
    public float RandomSoundDelay = 3f;
    float nextSound;

    // Update is called once per frame
    void Update()
    {
        if(sr.isVisible)
        {
            if (src != null && src.isPlaying == false)
                src.Play();

            nextSound -= Time.deltaTime;
            if(nextSound <= 0)
            {
                SoundManager.PlayClip(RandomSounds);
                nextSound = Random.Range(RandomSoundDelay * .5f, RandomSoundDelay * 1.5f);
            }
        }
        else
        {
            if(src != null && src.isPlaying)
                src.Stop();
        }
    }
}
