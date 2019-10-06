using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    float StickChance = 1f;
    float StickTime;
    Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        /*StickTime -= Time.deltaTime;
        if (rb.isKinematic && StickTime <= 0)
        {
            rb.velocity = Vector2.down;
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Random.Range(0f, 1f) > StickChance)
            return;

        // Sticking!

        if(rb == null)
        {
            Debug.LogError(gameObject.name + " is stick with no rigidbody?");
            return;
        }

        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;

        rb.position = collision.GetContact(0).point;

        // use for rotation: collision.GetContact(0).normal

        StickTime = Random.Range(0.5f, 2f);
    }
}
