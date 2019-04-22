using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {   

    }

    public void PressedBuildInfo() {
        SceneManager.LoadScene("BuildingInformation");

    }

    public void PressedLocateBuild() {
        SceneManager.LoadScene("LocateBuilding");

    }

    public void PressedViewMap() {
        SceneManager.LoadScene("ViewMap");

    }

    public void PressedAbout() {
        SceneManager.LoadScene("About");
    }
}
