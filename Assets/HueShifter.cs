using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueShifter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    Color targetHue;
    Color oldColor;
    SpriteRenderer sr;
    float timeLeft = 0;
    float changeTime = 0.5f;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if(timeLeft <= 0)
        {
            targetHue = Random.ColorHSV(0f, 1f);
            oldColor = sr.color;
            timeLeft = changeTime;
        }

        sr.color = Color.Lerp(targetHue, oldColor, timeLeft / changeTime);
    }
}
