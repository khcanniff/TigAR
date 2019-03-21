using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// this script handles the functionality of the upper right hand corner exit button
public class ExitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
