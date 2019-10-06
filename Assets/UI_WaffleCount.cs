using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_WaffleCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    TextMeshProUGUI text;
    LevelManager levelManager;

    // Update is called once per frame
    void Update()
    {
        text.text = ": " + levelManager.levelWafflesGot + "/" + levelManager.levelWaffles;
    }
}
