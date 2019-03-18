using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Text testText;

    // Start is called before the first frame update
    void Start() {
        testText.text = "No button pressed";
    }

    // Update is called once per frame
    void Update() {   

    }

    public void PressedBuildInfo() {
        testText.text = "Build Info Button has been pressed!";
    }

    public void PressedLocateBuild() {
        testText.text = "Locate Building Button has been pressed!";
    }

    public void PressedViewMap() {
        testText.text = "View Map Button has been pressed!";
    }

    public void PressedAbout() {
        testText.text = "About Button has been pressed!";
    }
}
