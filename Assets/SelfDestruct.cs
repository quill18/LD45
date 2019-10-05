using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(RandomizeTimes)
        {
            TimeLeft = Random.Range(TimeLeft * 0.5f, TimeLeft * 1.5f);
            FadeOutTime = Random.Range(FadeOutTime * 0.5f, FadeOutTime * 1.5f);
        }

        if (FadeOutTime > 0)
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }
    }

    public float TimeLeft = 1;
    public float FadeOutTime = 0f;
    public bool RandomizeTimes = false;

    SpriteRenderer[] spriteRenderers;

    // Update is called once per frame
    void Update()
    {
        TimeLeft -= Time.deltaTime;

        if (TimeLeft <= 0)
        {
            Destroy(gameObject);
        }
        else if(TimeLeft < FadeOutTime)
        {
            // Alpha fade all sprite renderers
            if(spriteRenderers != null)
            {
                foreach(SpriteRenderer sr in spriteRenderers)
                {
                    Color c = sr.color;
                    c.a = Mathf.Lerp(0f, 1f, TimeLeft / FadeOutTime);
                    sr.color = c;
                }
            }
        }
    }
}
