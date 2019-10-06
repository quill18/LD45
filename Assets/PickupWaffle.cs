using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWaffle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.FindObjectOfType<LevelManager>().GotWaffle();

        // Reset dash timer

        // Play Sound
        // Animation/particles?
        Destroy(gameObject);
    }
}
