using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        charCon = GetComponent<CharacterController2D>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    public bool godMode = false;

    CharacterController2D charCon;
    LevelManager levelManager;

    // Update is called once per frame
    void Update()
    {
        // Get Inputs
        charCon.SetXVelocity(Input.GetAxis("Horizontal") * charCon.WalkingSpeed);

        if (Input.GetButtonDown("Jump"))
        {
            charCon.DoJump();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerEnter2D: " + collision.gameObject.name);

        if (collision.GetComponent<DeathTrigger>() != null)
        {
            Die();
        }
        else if (collision.GetComponent<PickupBodyPart>() != null)
        {
            levelManager.UpdatePlayerTo(collision.GetComponent<PickupBodyPart>().NewPlayerPrefab);
            Destroy(collision.gameObject);
        }
    }

    void Die()
    {
        if(godMode)
        {
            Debug.Log("GOD MODE SAVE!");
            return;
        }

        Debug.Log("DIE!!!");
        // Spawn death animation

        Destroy(gameObject);


        levelManager.RestartLevel();
    }
}
