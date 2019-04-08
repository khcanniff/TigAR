using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocateBuildingManager : MonoBehaviour
{   
    public Text ChambersText;
    public Text BaunText;

    // Specific colors used when choosing words
    private float r = 0.0f, g = 1.0f, b = 0.0f, a = 1.0f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void UndoColors() {
        ChambersText.color = Color.black;
        BaunText.color = Color.black;
    }

    public void ChangeChamberColor() {
        ChambersText.color = Color.green;
    }

    public void ChangeBaunColor() {
        BaunText.color = Color.green;
    }
}
