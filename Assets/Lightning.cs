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
    }

    float strikeDelay = 10;
    float nextStrike;
    bool tripleStrike = false;

    MeshRenderer mr;

    void Randomize()
    {
        nextStrike = Random.Range(strikeDelay, strikeDelay * 1.5f);
        tripleStrike = Random.Range(0, 3) == 0;
    }

    // Update is called once per frame
    void Update()
    {
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
                if (tripleStrike == false)
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
                mr.enabled = true;
            }
            
        }
    }
}
