using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HealthGrid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public GameObject BrainHealthPrefab;

    PlayerPlatformerController player;

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindObjectOfType<PlayerPlatformerController>();

        }

        int hps = 0;

        if(player != null)
            hps = player.GetComponent<Health>().HPs;

        while(transform.childCount > hps )
        {
            Transform t = transform.GetChild(0);
            t.SetParent(null); // Become Batman
            Destroy(t.gameObject);
        }

        while (transform.childCount < hps)
        {
            GameObject go = Instantiate(BrainHealthPrefab);
            go.transform.SetParent(transform); // Adopt a young ward
            go.transform.localScale = Vector3.one;
        }

    }
}
