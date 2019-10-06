using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        nextStrike = 1f;
        mr = GetComponentInChildren<MeshRenderer>();

        if (mr == null)
            Debug.Log("No mesh renderer.");

        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    LevelManager levelManager;

    public AudioClip[] ThunderSounds;

    float strikeDelay = 10;
    float nextStrike;
    int numStrike = 2;

    MeshRenderer mr;

    void Randomize()
    {
        nextStrike = Random.Range(strikeDelay, strikeDelay * 1.5f);
        numStrike = (Random.Range(0, 3) == 0) ? 3 : 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.isOutdoors() == false)
            return;

        nextStrike -= Time.deltaTime;

        if(nextStrike <= 0)
        {

            if (nextStrike < -0.5f)
            {
                mr.enabled = false;
                Randomize();
            }
            else if(nextStrike < -0.4f)
            {
                mr.enabled = true;
            }
            else if(nextStrike < -0.3f)
            {
                mr.enabled = false;
                Randomize();
            }
            else if (nextStrike < -0.2f)
            {
                mr.enabled = true;
            }
            else if (nextStrike < -0.1f)
            {
                mr.enabled = false;
            }
            else
            {
                if(mr.enabled == false)
                    SoundManager.PlayClip(ThunderSounds);

                mr.enabled = true;
            }
            
        }
    }
}
