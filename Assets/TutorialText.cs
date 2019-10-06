using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponentInChildren<TextMeshProUGUI>();
        cg = GetComponent<CanvasGroup>();
    }

    TextMeshProUGUI Text;
    CanvasGroup cg;

    float displayTime = 3f;
    float fadeTime = 0.5f;

    // Update is called once per frame
    void Update()
    {
        displayTime -= Time.deltaTime;
        if (displayTime < fadeTime)
        {
            cg.alpha = Mathf.Lerp(0, 1, Mathf.Clamp01(displayTime / fadeTime));
        }
    }

    public static void ShowTutorial(string s)
    {
        TutorialText tt = GameObject.FindObjectOfType<TutorialText>();
        tt.displayTime = 3f;
        tt.Text.text = s;
        tt.cg.alpha = 1;
    }
}
