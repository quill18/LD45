using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    Rigidbody2D rb;

    public Vector3 TargetPosition;
    Vector3 startPosition;

    float TravelTime = 2f;
    float timer;

    float dir = 1;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime * dir;

        if (timer > TravelTime)
        {
            timer = TravelTime;
            dir *= -1;
        }
        else if (timer < 0)
        {
            timer = 0;
            dir *= -1;
        }

        float a = timer / TravelTime;

        rb.MovePosition(Vector3.Lerp(startPosition, TargetPosition, a));
    }
}
