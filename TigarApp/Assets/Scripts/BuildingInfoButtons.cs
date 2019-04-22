using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script is to handle the next and previous buttons on the informational widge popups
public class BuildingInfoButtons : MonoBehaviour
{
    private Text text;
    public string intialText;
    public GameObject PrevButton;
    public GameObject NextButton; // used to control when the next button is active
    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<Text>();
        intialText = intialText.Replace("\\n","\n");
        text.text = intialText;
        NextButton.SetActive(true);
        PrevButton.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void nextButton(string displayText)
    {   
        displayText = displayText.Replace("\\n","\n");
        text.text = displayText;
        NextButton.SetActive(false);
        PrevButton.SetActive(true);
    }
    public void prevButton(string displayText)
    {   
        displayText = displayText.Replace("\\n","\n");
        text.text = displayText;
        NextButton.SetActive(true);
        PrevButton.SetActive(false);
    }
}
