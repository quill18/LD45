using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenOtherMissing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform Target;

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
            Destroy(gameObject);
    }
}
