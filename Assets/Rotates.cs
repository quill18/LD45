using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotates : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float DegreesPerSec = 360;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, DegreesPerSec * Time.deltaTime));
    }
}
