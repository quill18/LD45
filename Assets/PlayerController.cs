using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        charCon = GetComponent<CharacterController2D>();
    }

    CharacterController2D charCon;

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
        Debug.Log("OnTriggerEnter2D: " + collision.gameObject.name);
    }
}
