using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    LevelManager levelManager;
    public Text Text;

    // Update is called once per frame
    void Update()
    {
        int perc = Mathf.FloorToInt((float)levelManager.globalWafflesGot / (float)levelManager.globalWaffles * 100f);
        Text.text = "Waffles Collected:\n"+ levelManager.globalWafflesGot + " of "+ levelManager.globalWaffles + " ("+ perc + " %)";
        
    }
}
