using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingInformationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OkButton() //if the user clicks the ok button on the building information scene. 
    {
        SceneManager.LoadScene("BuildingInformationScanner");
    }
}
