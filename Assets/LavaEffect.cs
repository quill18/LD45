using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LavaEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<TilemapRenderer>();

        originalColor = tr.material.color;
        altColor = new Color(0.7f, 0.7f, 1.0f);
    }

    TilemapRenderer tr;
    Color originalColor;
    Color altColor;
    float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        tr.material.color = Color.Lerp(originalColor, altColor, Mathf.Abs(Mathf.Sin(timer*2f)));
    }
}
