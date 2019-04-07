using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LocationGM : MonoBehaviour
{
    //serialized field allows button to be private but viewable in unity editor
    public Button confirmBttn;
    public Button undoBttn;
    public Gameobject BaunCircle;
    public Gameobject ChambersCircle;

    void Start()
    {
        confirmBttn = GetComponent<Button>();
        undoBttn = GetComponent<Button>();
    }

    public void ChoseBaun()
    {
        confirmBttn.setActive(true);
        undoBttn.setActive(true);

        BaunCircle.setActive(true);
    }

    public void ChoseChambers()
    {
        ChambersCircle.setActive(true);
    }
}
