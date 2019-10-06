using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_EndLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponentInChildren<TextMeshProUGUI>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    TextMeshProUGUI Text;
    LevelManager levelManager;

    // Update is called once per frame
    void Update()
    {
        int perc = Mathf.FloorToInt(
            (float)levelManager.levelWafflesGot / (float)levelManager.levelWaffles * 100f
            );

        Text.text = "Level Complete!\n\n"+ levelManager.levelWafflesGot + " of "+ levelManager.levelWaffles + " ("+perc+" %)";
    }
}
