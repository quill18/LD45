using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainBG : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();

    }

    MeshRenderer meshRenderer;
    public float speed =2f;
    float timer;

    // Update is called once per frame
    void LateUpdate()
    {
        timer += Time.deltaTime;

        Material m = meshRenderer.material;

        float OffsetX = (transform.position.x) / 20f + speed * timer + Mathf.Sin(timer*2f)/2f;
        float OffsetY = (transform.position.y) / 20f + speed * timer;

        m.mainTextureOffset = new Vector2(OffsetX, OffsetY);

        //m.mainTextureOffset += new Vector2(speed * Time.deltaTime, speed * Time.deltaTime);
    }
}
