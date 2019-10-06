using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorialText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TutorialText.ShowTutorial(Text);
    }

    public string Text = "Lorem Ipsum";
}
